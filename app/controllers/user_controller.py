class UserController:
    def __init__(self, user_repo):
        self.user_repo = user_repo

    def get(self, user_id):
        return self.user_repo.get_by_id(user_id)
