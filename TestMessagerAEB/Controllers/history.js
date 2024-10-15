const messageList = document.getElementById('messages');

async function getMessages() {
    try {
        const startTime = new Date(Date.now() - 60000);
        const endTime = new Date();

        const response = await fetch('/api/messages/${startTime.toISOString()}/${endTime.toISOString()}');

        if (response.ok) {
            const messages = await response.json();

            messages.forEach(message => {
                const li = document.createElement('li');
                li.textContent = '${message.Time}: ${message.Text}';
                messagesList.appendChild(li);
            });
        }
        else {
            console.error('Ошибка получения истории:', response.status);
        }
    }
    catch (error) {
        console.error('Ошибка:', error);
    }
}

getMessages();