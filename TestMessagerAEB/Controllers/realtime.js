const connection = new signalR.HubConnectionBuilder()
    .withUrl('/messagehub')
    .build();

const messageList = document.getElementById('message');

connection.on('ReceiveMessage', message => {
    const li = document.createElement('li');
    li.textContent = '${message.Time}: ${message.Text}';
    messagesList.appendChild(li);
});

connection.start()
    .then(() => {
        console.log('Соединение с хабом установлено');
    })
    .catch(err => {
        console.error('ошибка подключения:', err);
    });