import time
import requests
import json
import os
import sys
import urllib3

# Suprime o aviso de certificado SSL não verificado (para localhost)
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# --- CONFIGURAÇÕES ---

# 1. URL da sua API .NET (Backend)
API_URL = "http://localhost:5156/api/v1/Sessions"

# 2. Configuração da IA (Groq)
GROQ_API_KEY = "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"
GROQ_URL = "https://api.groq.com/openai/v1/chat/completions"

# Modelo atualizado (Llama 3.3 Versatile)
GROQ_MODEL = "llama-3.3-70b-versatile"

# --- ESTADO DO DISPOSITIVO ---
is_studying = False
start_time = 0

def format_duration(seconds):
    m, s = divmod(seconds, 60)
    h, m = divmod(m, 60)
    return f"{h:02d}:{m:02d}:{s:02d}"

def get_ai_feedback(duration_minutes):
    print(f"\n[IA] Processando dados com {GROQ_MODEL} (Groq)...")
    
    prompt = f"O usuário estudou por {duration_minutes} minutos. Aja como um coach de carreira. Dê um feedback curto (1 frase) e uma dica rápida."
    
    headers = {
        "Authorization": f"Bearer {GROQ_API_KEY}",
        "Content-Type": "application/json"
    }
    
    payload = {
        "model": GROQ_MODEL,
        "messages": [
            {"role": "system", "content": "Você é um assistente útil e motivador."},
            {"role": "user", "content": prompt}
        ],
        "temperature": 0.7
    }
    
    try:
        response = requests.post(GROQ_URL, headers=headers, json=payload)
        
        if response.status_code == 200:
            data = response.json()
            if 'choices' in data and len(data['choices']) > 0:
                return data['choices'][0]['message']['content']
            else:
                return "IA respondeu, mas sem conteúdo."
        else:
            error_msg = response.text
            print(f"[IA DEBUG] Erro API: {error_msg}")
            return f"Erro na IA ({response.status_code}): Verifique o modelo ou a chave."
            
    except Exception as e:
        return f"Erro de conexão com IA: {str(e)}"

def send_data_to_backend(duration_seconds, ai_feedback):
    duration_minutes = max(1, int(duration_seconds / 60))
    
    topic_message = f"Sessão IoT - Feedback IA: {ai_feedback}"
    
    if len(topic_message) > 250:
        topic_message = topic_message[:247] + "..."

    payload = {
        "topic": topic_message,
        "durationMinutes": duration_minutes,
        "isCompleted": True
    }

    print(f"[API] Sincronizando com o Backend .NET...")
    
    try:
        response = requests.post(API_URL, json=payload, headers={'Content-Type': 'application/json'}, verify=False)
        
        if response.status_code in [200, 201]:
            print(f"[API] SUCESSO! Dados persistidos no SQL Server.")
            try:
                resp_json = response.json()
                item_id = resp_json.get('id') if isinstance(resp_json, dict) else "N/A"
                print(f"[API] ID da Sessão: {item_id}")
            except:
                print(f"[API] ID da Sessão: (Resposta sem JSON)")
        else:
            print(f"[API] Erro no envio: {response.status_code} - {response.text}")
            
    except Exception as e:
        print(f"[API] FALHA CRÍTICA: Não foi possível conectar na API .NET.")
        print(f"      Verifique se ela está rodando em: {API_URL}")

def main():
    global is_studying, start_time
    
    os.system('cls' if os.name == 'nt' else 'clear')
    
    print("=================================================")
    print("   ReSkill+ IoT Device Simulator (Digital Twin)  ")
    print("=================================================")
    print("Status: ONLINE")
    print(f"IA Engine: {GROQ_MODEL}")
    print("Backend: .NET Core API")
    print("-------------------------------------------------")

    while True:
        try:
            if not is_studying:
                input("\nPressione [ENTER] para INICIAR o foco (Botão Verde)...")
                is_studying = True
                start_time = time.time()
                print("\n>>> LED AZUL: ON | MODO FOCO ATIVADO <<<")
                print("Cronômetro rodando... (Pressione Enter para parar)")
            else:
                input() 
                is_studying = False
                duration = int(time.time() - start_time)
                minutes = max(1, int(duration / 60))
                
                print(f"\n>>> LED AZUL: OFF | SESSÃO FINALIZADA")
                print(f"Tempo Total: {format_duration(duration)}")
                
                feedback = get_ai_feedback(minutes)
                print(f"\n🤖 MENTOR IA DIZ:\n\"{feedback}\"\n")
                
                send_data_to_backend(duration, feedback)
                
                print("\n-------------------------------------------------")
                
        except KeyboardInterrupt:
            print("\nDesligando dispositivo...")
            sys.exit(0)

if __name__ == "__main__":
    try:
        import requests
        import urllib3
    except ImportError:
        print("Erro: Bibliotecas necessárias não encontradas.")
        print("Instale com: pip install requests urllib3")
        sys.exit(1)
        
    main()