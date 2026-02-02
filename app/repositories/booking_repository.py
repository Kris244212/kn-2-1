from app.repositories.db_connection import DatabaseConnection

class BookingRepository:

    def __init__(self):
        self.db = DatabaseConnection().get_connection()
        self.cursor = self.db.cursor()

    def is_available(self, table_id, date, time):
        self.cursor.execute(
            "SELECT * FROM bookings WHERE table_id=? AND date=? AND time=?",
            (table_id, date, time)
        )
        return self.cursor.fetchone() is None

    def create(self, user_id, table_id, date, time):
        self.cursor.execute(
            "INSERT INTO bookings (user_id, table_id, date, time) VALUES (?, ?, ?, ?)",
            (user_id, table_id, date, time)
        )
        self.db.commit()
        return True

    def delete(self, booking_id):
        self.cursor.execute("DELETE FROM bookings WHERE id=?", (booking_id,))
        self.db.commit()
        return True
