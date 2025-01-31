Here’s a **GitHub-ready README** with a polished and Aspire-focused structure:  

---

# **Aspire with Azure Functions & Azure Storage**  

🚀 **A distributed application built with .NET 9 and C# 13, leveraging Azure Functions and Azure Storage for modern cloud-native development.** This project is configured for local development using an emulator, making it easy to test and iterate.  

## **🔹 Overview**  

This project demonstrates how to integrate **Aspire**, **Azure Functions**, and **Azure Storage** in a distributed application. It provides a scalable architecture while keeping the development experience smooth and efficient.  

### **📌 Key Features**  

- **Aspire-powered** application host for streamlined orchestration.  
- **Azure Functions** with **Queue Storage bindings** for event-driven processing.  
- **Local development support** via an **Azure Storage emulator**.  

## **⚠️ Solving an Azure Functions Configuration Challenge**  

While working with **Aspire and Azure Functions** in a **.NET 9 / C# 13 project**, I encountered an issue:  

> **Custom hosting environments do not always inherit default configurations.**  

This caused unexpected **message decoding errors** in Azure Queue Storage bindings. The fix? **Explicitly setting** `"messageEncoding": "none"` in `host.json`. This ensures proper message handling without decoding issues.  

```json
{
  "version": "2.0",
  "extensions": {
    "queues": {
      "messageEncoding": "none"
    }
  }
}
```

## **📁 Project Structure**  

- **`AppHost/`** – The Aspire-based host that configures and runs the distributed application.  
- **`QueueFunctionApp/`** – An Azure Functions project that processes messages from an Azure Storage queue.  

## **⚙️ Prerequisites**  

Ensure you have the following installed before running the project:  

- ✅ **.NET 9 SDK**  
- ✅ **Visual Studio 2022**  
- ✅ **Docker**  

## **📜 License**  

This project is licensed under the [MIT License](LICENSE).  

---

This README follows **GitHub best practices**, making it clear, engaging, and informative while maintaining the **Aspire** focus. Let me know if you'd like any tweaks! 🚀
