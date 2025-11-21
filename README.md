# 🚀 ReSkill+ | Global Solution 2025
### *Disruptive Architectures: IoT, IoB & Generative AI*

## 📖 Sobre o Projeto

O **ReSkill+ Focus Tracker** é uma solução integrada de **IoT (Internet das Coisas)** e **IoB (Internet do Comportamento)** projetada para **monitorar, analisar e otimizar o tempo de estudo e foco** de profissionais em requalificação.

Alinhado ao tema **“O Futuro do Trabalho”**, o projeto utiliza:

- Um **dispositivo Digital Twin** (simulado em Python) para coletar dados em tempo real
- **Inteligência Artificial Generativa (Llama 3 via Groq)** para agir como um *Mentor de Carreira*
- Um **backend .NET 8** para armazenar e disponibilizar o histórico de estudos

O resultado é uma plataforma que une **IoT + IoB + GenAI** para melhorar a performance e a motivação do usuário.

## 🎥 Vídeo Demonstrativo (Obrigatório)

Confira a demonstração completa da solução *(IoT + IA + Backend)* funcionando:

👉 **[CLIQUE AQUI PARA ASSISTIR AO VÍDEO NO YOUTUBE](#)**

## 🏛️ Arquitetura da Solução

### 🔹 1. Dispositivo IoT (Digital Twin)

- Simulador desenvolvido em **Python**
- Atua como dispositivo físico (Edge Computing)
- Gerencia estado da sessão (Início/Fim do estudo)
- Coleta métricas comportamentais (IoB)
- Envia dados para a IA Generativa (Groq / Llama 3)
- Envia os resultados para a API .NET

### 🔹 2. Backend / API (.NET 8)

- Desenvolvido em **C# (.NET 8)**
- Arquitetura **RESTful**
- Recebe e persiste dados vindos do IoT
- Banco de dados **SQL Server**

### 🔹 3. Inteligência Artificial (Generative AI)

- **Modelo:** Llama 3.3 (70b-versatile) via Groq Cloud
- Função:
  - Analisar tempo de foco
  - Gerar feedbacks motivacionais
  - Recomendar comportamentos e soft skills

## 🔁 Fluxo de Dados

1. Usuário inicia a sessão no dispositivo IoT
2. IoT começa a contar o tempo (Foco)
3. Usuário encerra sessão
4. IoT envia a duração para a IA (Groq)
5. IA retorna feedback personalizado
6. IoT envia **Duração + Feedback** para a API .NET
7. A API salva no banco SQL Server

## 📂 Estrutura do Repositório

```
/
├── backend/
│   ├── ReSkill.API/
│   └── ReSkill.Tests/
├── iot/
│   └── iot_simulator.py
└── README.md
```

## 🚀 Como Rodar o Projeto

### ✔️ Pré-requisitos

- .NET 8 SDK
- Python 3.8+
- SQL Server (LocalDB ou Container)
- Chave de API da Groq

## ▶️ Passo 1: Backend (.NET)

```
cd backend/ReSkill.API
dotnet restore
dotnet ef database update
dotnet run
```

A API deve rodar em:

- http://localhost:5156
- https://localhost:7226

## ▶️ Passo 2: Dispositivo IoT (Python)

```
cd iot
pip install requests urllib3
python iot_simulator.py
```

## 🧪 Testes e Validação

### Swagger
http://localhost:5156/swagger

### Console Python
- Duração
- Feedback da IA

## 👥 Integrantes do Grupo

| Nome | RM |
|------|------|
| Felipe Rosa Peres | RM 557636 |
| Vinícius De Souza Sant Anna | RM 556841 |
| Pedro Henrique De Souza | RM 555533 |
