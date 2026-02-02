from flask import Flask


def create_app():
    app = Flask(__name__, static_folder='app/views/static', template_folder='app/views/templates')

    # Import and initialize routes/blueprints
    from app import routes
    routes.init_app(app)

    return app
