from flask import render_template


def register_routes(app):

    @app.route("/")
    def index():
        return render_template("index.html")

    @app.route("/login")
    def login():
        return render_template("login.html")

    @app.route("/register")
    def register():
        return render_template("register.html")

    @app.route("/booking")
    def booking():
        return render_template("booking.html")

    @app.route("/bookings")
    def bookings():
        return render_template("bookings.html")

    @app.route("/admin")
    def admin():
        return render_template("admin.html")
