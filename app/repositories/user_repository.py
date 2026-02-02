from app.models.user import User


class UserRepository:
    def get_by_id(self, id):
        # Stub implementation
        return User(id=id, name="Test User", email="user@example.com")
