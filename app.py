# app.py

from flask import Flask, jsonify, request, render_template
import threading
import time

app = Flask(__name__, static_url_path='/static')

# --- Змінні стану ліфта ---
current_floor = 1 
target_floors = [] 
direction = "idle" # "up", "down", "idle"

# --- Алгоритм руху ліфта ---
def move_elevator():
    global current_floor, direction, target_floors
    
    while True:
        if not target_floors:
            direction = "idle"
            time.sleep(1) # Затримка в режимі очікування
            continue
        
        # Обробляємо перший запит у черзі
        target = target_floors[0] 
        
        if target > current_floor:
            current_floor += 1
            direction = "up"
        elif target < current_floor:
            current_floor -= 1
            direction = "down"
        else:
            # Прибуття: видаляємо запит
            target_floors.pop(0) 
            direction = "idle"
            time.sleep(2) # Імітація часу зупинки
            
        # Час на перехід між поверхами
        time.sleep(1) 

# --- Маршрути Flask ---

@app.route("/")
def index():
    """Головна сторінка, відображає веб-інтерфейс (index.html)."""
    return render_template("index.html")

@app.route("/call", methods=["POST"])
def call_elevator():
    """Обробляє POST-запит на виклик ліфта."""
    try:
        # КРИТИЧНЕ ВИПРАВЛЕННЯ: Явне перетворення на int()
        floor_value = request.json.get("floor")
        floor = int(floor_value)
    except (TypeError, ValueError):
        return jsonify(success=False, message="Invalid floor value"), 400

    # Додаємо запит у чергу, якщо його ще немає (і він у діапазоні 1-10)
    if 1 <= floor <= 10 and floor not in target_floors:
        target_floors.append(floor)
        
    return jsonify(success=True)

@app.route("/status")
def status():
    """Повертає поточний стан ліфта (JSON) для оновлення інтерфейсу."""
    # Також повертаємо чергу, щоб JS міг зняти підсвічування кнопки
    return jsonify(floor=current_floor, 
                   direction=direction,
                   target_floors=target_floors)

# Запуск алгоритму руху ліфта в окремому потоці
threading.Thread(target=move_elevator, daemon=True).start()

if __name__ == "__main__":
    app.run(debug=True, host='0.0.0.0')