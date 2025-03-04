using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


// Used to handle file transfers between users
namespace EasyFileTransferApp.Model
{
    class FileTransfer
    {
        private const int FileTransferPort = 8081; // Port used for file transfers

        // Send a file to a client in 8kb chunks
        public async Task SendFileAsync(string filePath, string ipAddress)
        {
            TcpClient client = new TcpClient(ipAddress, FileTransferPort); // Connect to the client
            NetworkStream stream = client.GetStream(); // Get the network stream for reading/writing
            FileStream fileStream = File.OpenRead(filePath); // Open the file for reading

            // Send the file name and size to the client
            string fileName = Path.GetFileName(filePath); // Get the file name
            byte[] fileNameData = Encoding.UTF8.GetBytes(fileName); // Convert the file name to a byte array
            byte[] fileNameLength = BitConverter.GetBytes(fileNameData.Length); // Convert the file name size to a byte array
            await stream.WriteAsync(fileNameLength, 0, fileNameLength.Length); // Send the file name size to the client
            await stream.WriteAsync(fileNameData, 0, fileNameData.Length); // Send the file name to the client

            byte[] fileSize = BitConverter.GetBytes(fileStream.Length); // Convert the file size to a byte array
            await stream.WriteAsync(fileSize, 0, fileSize.Length); // Send the file size to the client

            // Send File data in chunks
            byte[] data = new byte[8192]; // Buffer to hold the file data
            int bytesRead; // Number of bytes read from the file
            while ((bytesRead = fileStream.Read(data, 0, data.Length)) > 0) // While there is still bytes to be read read in buffer and send to client
            {
                await stream.WriteAsync(data, 0, bytesRead); // Send the file data to the client
            }
        }

        // Receive a file from a client in 8kb chunks
        public async Task RecieveFileAsync(string saveDirectory)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, FileTransferPort); // Create a TCP listener to listen for incoming file transfers
            listener.Start(); // Start listening for incoming connections

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync(); // Accept the incoming connection
                NetworkStream stream = client.GetStream(); // Get the network stream for reading/writing

                // Read the file name length from the client
                byte[] fileNameLengthData = new byte[sizeof(int)]; // Buffer to hold the file name size
                await stream.ReadAsync(fileNameLengthData, 0, fileNameLengthData.Length); // Read the file name size from the client
                int fileNameLength = BitConverter.ToInt32(fileNameLengthData, 0); // Convert the file name size to an integer

                // Read file name from the client
                byte[] fileNameData = new byte[fileNameLength]; // Buffer to hold the file name
                await stream.ReadAsync(fileNameData, 0, fileNameData.Length); // Read the file name from the client
                string fileName = Encoding.UTF8.GetString(fileNameData); // Convert the file name to a string

                // Read the file size from the client
                byte[] fileSizeData = new byte[sizeof(long)]; // Buffer to hold the file size
                await stream.ReadAsync(fileSizeData, 0, fileSizeData.Length); // Read the file size from the client
                long fileSize = BitConverter.ToInt64(fileSizeData, 0); // Convert the file size to a long

                // Create a file stream to save the file
                string savePath = Path.Combine(saveDirectory, fileName); // Combine the save directory and file name to get the save path
                FileStream fileStream = File.Create(savePath); // Create the file stream to save the file

                // Receive file data in chunks
                byte[] data = new byte[8192]; // Buffer to hold the file data
                int bytesRead; // Number of bytes read from the client
                long totalBytesRead = 0; // Total number of bytes read from the client
                while (totalBytesRead < fileSize) // While there is still bytes to be read read in buffer and write to file
                {
                    bytesRead = await stream.ReadAsync(data, 0, data.Length); // Read the file data from the client
                    await fileStream.WriteAsync(data, 0, bytesRead); // Write the file data to the file
                    totalBytesRead += bytesRead; // Update the total number of bytes read
                }
            }
                
        }

    }
}
