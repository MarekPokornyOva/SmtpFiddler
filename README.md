![Logo](./logo.png "Logo")
# SmtpFiddler

### Description
SmtpFiddler is a tool to view sent mail messages. It behaves as a SMTP server, can receive and show sent mails.
It was originaly made as a tool for application developers to view sent mails and don't send them to a real recepients.

### Features
* Receives an mails using SMTP protocol
* Pure .NET code
* Shows the mails with various view formats (plain text,HTML,hex,raw,image)
* Allows to forward the mails
* Allows to save/load mails to/from EML file
* UI extendable by .NET 4.x assemblies
* Act as a dummy or a SMTP proxy
* Show protocol communication details

### 2 in 1
There are 2 applications:
* Winforms - main application executable in Windows built on top of .NET FW (4.6.1).
* Avalonia - experimental application built on top of .NET Core (2.1) and AvaloniaUI framework executable in Windows/Linux/macOS.

### Note
No ESMTP support.
