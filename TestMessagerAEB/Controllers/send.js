const messageForm = document.getElementById('messageForm');
const messageText = document.getElementById('messageText');

messageForm.addEventListener('submit', async (event) => {
    event.preventDefault();

    const message = messageText.value;
    if (message.trim() === '') {
        aler('Пожалуйста, введите сообщение');
        return;
    }

    try {
        await connection.invoke("SendMessage", user, message);
        const response = await fetch('/api/messages', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Text: message,
                SequenceNumber: 1
            })
        });

        if (response.ok) {
            alert('Сообщение отправлено');
            messageText.value = '';
        }
        else {
            console.error('Ошибка отправки сообщения', response.status);
        }

    } catch (error) {
        console.error('Ошибка', error);
    }
});