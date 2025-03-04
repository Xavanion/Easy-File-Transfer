# NetShare - A WPF based file sharing Application
A WPF application that enables seamless file sharing between users on the same local network using UDP broadcast for discovery and TCP for file transfer. The application also supports remote file sharing by allowing users to create and join rooms with a group code.

## Features
* Local Network Client Discover: Allow a user to automatically discover others clients on their network
* Direct File Sharing: Allows to directly send other local clients files seamlessly
* Remote File Sharing: Allows users to create groups and share files remotely

## Usage
### Local File Sharing
1. Launch the application
2. Navigate to the "Share" Menu
3. Click on local Scan
4. Click on the client you wish to share with and send them your file

### Remote File Sharing
1. Create a room and share the generated group code with other users
2. Other users will use that group code and connect to your room
3. Afterwards you will be able to share files seamlessly

## Technologies Used:
* C# .NET (WPF) - UI framework & code behind
* UDP Broadcasting - For discovering local clients
* TCP - For file sharing
