import * as signalr from '@microsoft/signalr';
const conn = new signalr.HubConnectionBuilder()
              .withUrl('http://localhost:5179/MyHub')
              .withAutomaticReconnect()
              .configureLogging({
                  log: function(logLevel, message) {
                      console.log(new Date().toISOString() + ": " + message);
                  }
              })
              .build();
export default conn;
