# xCert

> A cross-platform dotnetcommand-line tool written in C# for generating certificates and secure passwords

[![github release version](https://img.shields.io/github/v/release/CLKR-Dev/xCert.svg?include_prereleases)](https://github.com/CLKR-Dev/xCert/releases/latest) 
[![license](https://img.shields.io/github/license/CLKR-Dev/xCert.svg)](https://github.com/CLKR-Dev/xCert/blob/master/LICENSE) 
[![PRs welcome](https://img.shields.io/badge/PRs-welcome-97c50f.svg)](https://github.com/CLKR-Dev/xCert/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22) 
[![Made with love by CLKR](https://img.shields.io/badge/made%20with%20%E2%99%A5%20by-CLKR-f47421.svg)](https://github.com/CLKR-Dev)

<center><img src="https://placehold.co/1920x300?text=Work%20in%20progress" /></center>

## 🚩 Table of Contents

- [Introduction](#-Introduction)
- [Commands](#-Commands)
- [License](#-license)

## 💡 Introduction

xCert was created to simplify and streamline the process of generating secure certificates and passwords directly from the command line — with zero ceremony.

Most developers and devops teams only need a few good certificates — not an entire CA stack or complicated OpenSSL config. xCert offers:

- ✅ **One-liner certificate generation** for common use cases (server, client, CA, code signing, licensing)
- 🔐 **Automatic password handling**, including generation and storage
- 📜 **Readable CLI output** using Spectre.Console
- 💡 **Self-contained and cross-platform**, built with .NET
- ⚙️ **Script-friendly**: Works great in CI/CD pipelines and scripts
- 🧼 **No dependencies** other than the .NET runtime

If you’ve ever struggled with OpenSSL commands, needed test certs quickly, or wanted to integrate simple cert creation into your tools — xCert is for you.

## ⚙️ Commands

### Certificate Generation

| Command | Description |
| --- | --- |
| `xCert gen <UseCase> <Name> [Password]` | Generates a certificate and private key pair. Use case must be one of: `server`, `client`, `ca`, `codesigning`, `licensing`. Password is optional (auto-generated if omitted). |

**Examples:**

| Example | Description |
| --- | --- |
| `xCert gen server MyServerCert SuperSecret` | Creates a server certificate named `MyServerCert` using the password `SuperSecret`. |
| `xCert gen client MyClientCert` | Creates a client certificate named `MyClientCert` with an auto-generated password. |

### Password Generation

| Command | Description |
| --- | --- |
| `xCert password [--length <int>] [--out <filename>] [--print]` | Generates a random password. You can specify length, output file, and whether to print it to console. |

**Examples:**

| Example | Description |
| --- | --- |
| `xCert password --length 32 --out mypass.pwd --print` | Generates a 32-character password, saves it to `mypass.pwd`, and prints it to the console. |
| `xCert password --length 16` | Generates a 16-character password and saves it to `password.pwd` (default). |


## 📜 License

This software is licensed under the [MIT](https://github.com/CLKR-Dev/xCert/blob/dev/LICENSE.md) © [CLKR](https://github.com/CLKR-Dev).
