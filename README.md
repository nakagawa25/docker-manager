# Docker Manager
An Application to manage the Docker in Cloud plataforms.

## 📖 About
This project was created to manage Docker containers in cloud platforms and virtual machines. Using the Docker API, it can manage resources like: create, list and delete images and containers and get current stats of running containers.

## ⚒️ Technologies
- .NET Core (C#)
- HTML
- CSS
- JavaScript
- Vue.js

## ⚙️ Prerequisites
- [.NET Core SDK (version >= 3.1.409)](https://dotnet.microsoft.com/download/dotnet/3.1)
- [Web Server for Chrome](https://chrome.google.com/webstore/detail/web-server-for-chrome/ofhbbkphhbklhfoeikjpcbhemlocgigb) or [Live Server extension for VSCode](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer)

## Running
### API
1. Go to the API folder
```
$ cd API
```

2. Build the project
```
$ dotnet build
```

3. Run the application
```
$ dotnet run
```

4. The API will run in **http://localhost:5000**.

### Front-end
**- Using Web Server for Chrome**
1. Open the Web Server for Chrome app
2. Choose the `FrontEnd` folder
3. Start the web server
4. Open the web server URL
5. Select the `html` folder
6. It will show the app interface in your web browser

**-Using Live Server extension from VSCode**
1. Open the project in Visual Studio Code
2. Open the `index.html`
3. In the upper menu, click View -> Command Palette
4. Type **Live Server: Open with Live Server**, then select it
5. It will open the app interface in your web browser
