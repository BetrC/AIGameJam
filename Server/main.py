from http.server import BaseHTTPRequestHandler, HTTPServer
from socketserver import ThreadingMixIn
from urllib import parse
from CommandHandler import CommandHandler

hostName = "192.168.1.10"
serverPort = 8080
commandHandler = CommandHandler()

class Handler(BaseHTTPRequestHandler):
    def do_GET(self):
        full_path = "http://" + hostName + ":" + str(serverPort) + self.path
        split_url = parse.urlsplit(full_path)
        param = parse.parse_qs(split_url.query)
        self.send_response(200)
        self.end_headers()
        result = commandHandler.on_Command(split_url.path, param)
        self.wfile.write(result)

class ThreadingSimpleServer(ThreadingMixIn, HTTPServer):
    pass

if __name__ == "__main__":
    server = ThreadingSimpleServer((hostName, serverPort), Handler)
    print("Server started http://%s:%s" % (hostName, serverPort))
    server.serve_forever()

    print("Server stopped.")