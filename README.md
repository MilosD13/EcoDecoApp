EncoDeco App
==========================

This project is a utility tool for encoding a list of integers from a CSV file into a compact representation stored in a TXT file and decoding a compact representation from a TXT file back into a list of integers in a CSV file.

Getting Started
---------------
Prerequisites
Ensure you have the .NET runtime installed on your system. You can download it from the official [.NET website](https://dotnet.microsoft.com/en-us/download).

Building the Project
---------------

1. Clone the Repository:

```
git clone <repository-url>
cd <repository-directory>
``` 

2. Build the Project:
---------------
Run the following command to build the project:
```
dotnet build
```
Running the Utility
---------------
1. Launch the Utility:
Open a terminal or command prompt, navigate to the directory containing the built executable, and run:
```
dotnet run
```
Or run the app in your choice of IDE

2. Follow the Menu Instructions:
---------------
The utility will display a menu with the following options:
```
=========================================
     Integer Encoder/Decoder Utility
=========================================
1. Encode (CSV to TXT)
2. Decode (TXT to CSV)
3. Exit
=========================================
```

Usage Instructions
---------------
### Encoding (CSV to TXT)
1. Prepare Your CSV File:
Create a CSV file with your integers separated by commas. Example: integers.csv containing 1,2,3,5,8,13,21.

2. Place Your CSV File:
Place the file within the executable dir. 
Example: if the project is ran in debug mode, place your CSV file within `EncoDecoApp\EncoDeco\bin\Debug\net8.0`

3. Select Encoding Option:
In the utility menu, enter 1 to encode a CSV file into a TXT file.

4. Select the CSV File:
The utility will list all CSV files in the current directory. 
Enter the number corresponding to the file you want to encode.

5. Output:
The encoded output will be saved to a new TXT file with a timestamped filename. Example: `encoded_20240518123000.txt`.

### Decoding (TXT to CSV)
1. Prepare Your TXT File:
Ensure you have a TXT file containing the encoded string in the specified format. Example: `encoded.txt` containing `1:3,5,8,13,21`.

2. Select Decoding Option:
In the utility menu, enter 2 to decode a TXT file into a CSV file.

3. Select the TXT File:
The utility will list all TXT files in the current directory. Enter the number corresponding to the file you want to decode.

4. Output:
The decoded output will be saved to a new CSV file with a timestamped filename. Example: `decoded_20240518123000.csv`.

Readability of Encoded TXT File
---------------
The encoded TXT file uses a compact representation to store ranges of integers. Here's how to interpret the encoded file:

Ranges: A range of consecutive integers is represented by two numbers separated by a colon (`:`). For example, `1:3` represents the integers `1,2,3`.
Individual Numbers: Individual numbers are listed without a colon. For example, `5` represents the integer `5`.
Combined Representation: Ranges and individual numbers are separated by commas. For example, `1:3,5,8,13,21` represents the integers `1,2,3,5,8,13,21`.