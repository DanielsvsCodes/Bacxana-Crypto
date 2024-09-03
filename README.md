# Bacxana-Crypto

## Overview

Bacxana-Crypto is a simple implementation of a blockchain-based cryptocurrency written in C# using .NET 8.0. This project demonstrates fundamental blockchain concepts, such as distributed ledger management, wallet creation, transaction signing, and block mining. The backend services handle core blockchain functionalities and cryptographic operations, leveraging **Libsodium** for secure key management and transaction signing.

### Key Components

1. **Blockchain Ledger Service**: Manages the blockchain ledger, handles block creation, and validates transactions.
2. **Wallet Service**: Manages wallet creation, public-private key generation, and transaction signing using Libsodium.
3. **Transaction Service**: Facilitates transactions between wallets, ensuring validity and security.
4. **Networking Service**: Enables peer-to-peer communication between nodes, allowing decentralized operation of the blockchain network.

## Technologies Used

- **.NET 8.0**: The latest version of the .NET framework for building robust backend services.
- **Libsodium**: A modern, easy-to-use cryptographic library for encryption, decryption, signatures, password hashing, and more.
- **TCP/IP Networking**: Used for peer-to-peer communication between nodes in the blockchain network.
- **C#**: The programming language used to implement blockchain logic and cryptographic operations.

## How It Works

- **Blockchain Ledger Service**: Maintains a list of blocks, each containing a set of transactions. It ensures blockchain integrity by validating each block's hash and its link to the previous block.
- **Wallet Service**: Uses Libsodium to generate secure public-private key pairs. Wallets can sign transactions with their private keys to ensure authenticity and integrity.
- **Transaction Service**: Represents the transfer of value between wallets. Each transaction must be signed by the sender's private key and verified by the network nodes.
- **Networking Service**: Facilitates peer-to-peer communication among nodes, broadcasting transactions and newly mined blocks to keep the blockchain consistent across the network.

## Features

- **Secure Key Management**: Utilizes Libsodium to generate and manage cryptographic keys.
- **Digital Signatures**: Ensures the authenticity of transactions using secure digital signatures.
- **Blockchain Mining**: Implements a basic proof-of-work consensus mechanism to add blocks to the blockchain.
- **Peer-to-Peer Networking**: Enables decentralized communication between network nodes.

## Project Structure

```plaintext
Bacxana-Crypto/
├── Bacxana/
│   ├── bin/                      # Compiled binaries
│   ├── obj/                      # Object files and intermediate build results
│   ├── src/
│   │   ├── blocksystem/          # Core blockchain components
│   │   │   ├── Block.cs          # Definition of a block in the blockchain
│   │   │   ├── Ledger.cs         # Manages the blockchain ledger and transactions
│   │   ├── cryptography/         # Cryptography-related components
│   │   │   ├── Wallet.cs         # Wallet management and cryptographic operations
│   │   ├── networking/           # Networking components for P2P communication
│   │   │   ├── Node.cs           # Network node for peer-to-peer communication
│   │   ├── transactions/         # Transaction-related components
│   │   │   ├── Transaction.cs    # Transaction structure and handling
│   │   ├── frontend/             # Frontend or UI components (if any)
│   ├── Bacxana.csproj            # Project file for .NET configuration and dependencies
│   ├── Program.cs                # Entry point of the application
├── Bacxana-Crypto.generated.sln  # Solution file for the project
└── README.md                     # Project documentation
```

## Prerequisites

- **.NET 8 SDK**: Make sure the .NET 8 SDK is installed on your machine.
- **Libsodium**: The project uses the `Sodium.Core` NuGet package for cryptographic operations. Ensure this package is installed.

## Getting Started

### Step 1: Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/Bacxana-Crypto.git
cd Bacxana-Crypto/Bacxana
```

### Step 2: Install Dependencies

Install the necessary dependencies using NuGet:

```bash
dotnet add package Sodium.Core --version 1.3.0
```

### Step 3: Build the Project

Build the project using the .NET CLI:

```bash
dotnet build
```

### Step 4: Run the Application

Run the application from the command line:

```bash
dotnet run
```

### Step 5: Create and Manage Wallets

- **Create a New Wallet**:
    ```csharp
    var wallet = new Wallet();
    Console.WriteLine($"New wallet created with public key: {wallet.PublicKey}");
    ```

- **Sign a Transaction**:
    ```csharp
    string dataToSign = "Transfer 10 Bacxana coins to wallet B";
    string signature = wallet.SignData(dataToSign);
    Console.WriteLine($"Data signed: {signature}");
    ```

- **Verify a Signature**:
    ```csharp
    bool isSignatureValid = Wallet.VerifySignature(wallet.PublicKey, dataToSign, signature);
    Console.WriteLine($"Is the signature valid? {isSignatureValid}");
    ```

### Step 6: Mine Blocks

Mine a block to add transactions to the blockchain:

```csharp
var ledger = new Ledger();
ledger.MinePendingTransactions(wallet.PublicKey);
```
## Troubleshooting

### Common Issues

- **Build Errors**: Ensure all dependencies are installed and the project targets the correct .NET framework version.
- **Null Reference Exceptions**: Check that all objects are properly initialized before use.
- **Networking Issues**: Ensure your firewall or antivirus software is not blocking TCP/IP connections between nodes.

### Docker Services

- If using Docker to manage services, ensure Docker Desktop is running and the Docker Compose file is correctly configured.
- Run `docker ps` to check if all services are running. If a service is not running, check the logs for errors using `docker logs <container_id>`.
