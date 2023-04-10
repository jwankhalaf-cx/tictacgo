# TicTacGo

TicTacGo is an attempt at building the classic TicTacToe game in .NET 7. The solution uses Blazor for its frontend and SignalR for the real-time interactivity.

## Prerequisite

You will need version 7.0.103 of the .NET Software Development Kit (SDK) installed. This can be downloaded from [here](https://dotnet.microsoft.com/en-us/download).

Once you've installed the .NET SDK, you can run `dotnet --list-sdks` to see what SDK versions you have installed. The output will be something like:

```
7.0.103 [/usr/share/dotnet/sdk]
```

You will also need NodeJS installed for Tailwind to work. You can install NodeJS in a varierty of ways, but we recommend installing it via the Node Version Manager (NVM). Read and follow instructions [here](https://github.com/nvm-sh/nvm).

## Building

To build the project, run `dotnet build .` from the root directory of the project.

## Running

To run the project, run `dotnet run .` from the root directory of the project.
