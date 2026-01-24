// static/script.js (або elevator.js)

// ВАЖЛИВО: Висота одного поверху у пікселях. Має відповідати CSS.
const FLOOR_HEIGHT = 50; 

/**
 * Надсилає POST-запит на сервер Flask для виклику ліфта.
 * @param {number} floor - Номер поверху.
 */
function callElevator(floor) {
    // 1. Позначаємо кнопку як "очікує"
    const button = document.querySelector(`.call-btn[data-floor="${floor}"]`);
    if (button) {
        button.classList.add('waiting'); 
    }

    // 2. Надсилаємо запит
    fetch("/call", {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({floor: floor}) 
    })
    .catch(error => console.error('Помилка мережі при виклику:', error));
}

/**
 * Регулярно надсилає GET-запит на сервер для оновлення стану та візуалізації.
 */
function updateElevator() {
    fetch("/status")
        .then(r => r.json())
        .then(data => {
            const elevator = document.getElementById("elevator");
            
            // Оновлення інформаційної панелі
            document.getElementById("current-floor").textContent = data.floor;
            document.getElementById("current-direction").textContent = data.direction.toUpperCase();
            document.getElementById("current-queue").textContent = `[${data.target_floors.join(', ')}]`;

            // 1. Оновлення візуальної позиції
            // Використовуємо CSS transition для плавності
            elevator.style.bottom = (data.floor - 1) * FLOOR_HEIGHT + "px";

            // 2. Зняття підсвічування з кнопки (після прибуття)
            const currentFloorButton = document.querySelector(`.call-btn[data-floor="${data.floor}"]`);
            
            // Якщо ліфт стоїть (idle) і черга не містить поточного поверху (це означає, що запит був оброблений), 
            // знімаємо клас 'waiting', якщо він був встановлений.
            if (data.direction === 'idle' && currentFloorButton && currentFloorButton.classList.contains('waiting') && !data.target_floors.includes(data.floor)) {
                 currentFloorButton.classList.remove('waiting');
            }
            
        })
        .catch(error => {
            // Виводить помилку, якщо сервер Flask не відповідає
            console.error('Помилка отримання стану ліфта (Сервер не відповідає).', error);
        });
}

// Регулярне оновлення стану кожну секунду
setInterval(updateElevator, 1000); 

// Перше оновлення при завантаженні сторінки
document.addEventListener('DOMContentLoaded', updateElevator);