# Setup

### .Net 8
Install .net 8 onto your computer, either through rider or through the command line.

Install Docker Desktop onto your computer via https://docs.docker.com/desktop/install/mac-install/

From the main directory run `docker compose up` to make sure docker has been installed correctly.

Install dotnet cli tool using the command `brew install --cask dotnet

### Client

Run the following commands to install the sdks for the client.

`sudo dotnet workload install ios` This will install the ios sdk
`sudo dotnet workload install android` This will install the android sdk
`sudo dotnet workload install wasm-tools` This will install the wasm sdk for the browser client


# Running the Web App

From the main directory run `docker compose up` to run the database.

Open the solution in Rider and run the project `MonoApp.https` to run the web app.

This should open a swagger document in your browser. If this works, you have successfully set up the web client.

