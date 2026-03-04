from flask import Flask, render_template, redirect, request, url_for
from flask_login import LoginManager, login_user, login_required, logout_user, current_user
from werkzeug.security import generate_password_hash, check_password_hash
from database.models import db, User, Train, Booking

app = Flask(__name__)
app.config['SECRET_KEY'] = 'supersecretkey'
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///database.db'

db.init_app(app)

login_manager = LoginManager()
login_manager.init_app(app)
login_manager.login_view = "login"

@login_manager.user_loader
def load_user(user_id):
    return User.query.get(int(user_id))


# ✅ Створення таблиць (працює у Flask 3)
with app.app_context():
    db.create_all()

    # Додаємо тестові поїзди один раз
    if Train.query.count() == 0:
        trains = [
            Train(from_city="Київ", to_city="Львів", date="2026-03-01", seats=50),
            Train(from_city="Київ", to_city="Одеса", date="2026-03-02", seats=40),
            Train(from_city="Львів", to_city="Харків", date="2026-03-03", seats=35)
        ]
        db.session.add_all(trains)
        db.session.commit()


@app.route('/')
def index():
    return render_template('index.html')


@app.route('/register', methods=['GET', 'POST'])
def register():
    if request.method == 'POST':
        hashed_password = generate_password_hash(request.form['password'])
        user = User(
            username=request.form['username'],
            password=hashed_password
        )
        db.session.add(user)
        db.session.commit()
        return redirect(url_for('login'))
    return render_template('register.html')


@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'POST':
        user = User.query.filter_by(username=request.form['username']).first()
        if user and check_password_hash(user.password, request.form['password']):
            login_user(user)
            return redirect(url_for('dashboard'))
    return render_template('login.html')


@app.route('/dashboard')
@login_required
def dashboard():
    trains = Train.query.all()
    cities_from = sorted(set([train.from_city for train in trains]))
    cities_to = sorted(set([train.to_city for train in trains]))
    return render_template(
        'dashboard.html',
        trains=trains,
        cities_from=cities_from,
        cities_to=cities_to
    )


@app.route('/search', methods=['POST'])
@login_required
def search():
    trains = Train.query.filter_by(
        from_city=request.form['from_city'],
        to_city=request.form['to_city'],
        date=request.form['date']
    ).all()
    return render_template('search.html', trains=trains)


@app.route('/book/<int:train_id>')
@login_required
def book(train_id):
    train = Train.query.get(train_id)

    if train.seats > 0:
        train.seats -= 1
        booking = Booking(user_id=current_user.id, train_id=train_id)
        db.session.add(booking)
        db.session.commit()

    return redirect(url_for('dashboard'))


@app.route('/logout')
@login_required
def logout():
    logout_user()
    return redirect(url_for('index'))


if __name__ == '__main__':
    app.run(debug=True)