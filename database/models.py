from flask_sqlalchemy import SQLAlchemy
from flask_login import UserMixin

db = SQLAlchemy()

class User(UserMixin, db.Model):
    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(100), unique=True, nullable=False)
    password = db.Column(db.String(200), nullable=False)

class Train(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    from_city = db.Column(db.String(100))
    to_city = db.Column(db.String(100))
    date = db.Column(db.String(50))
    seats = db.Column(db.Integer)

class Booking(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.Integer)
    train_id = db.Column(db.Integer)