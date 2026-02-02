class BookingService:

    def __init__(self, booking_repo):
        self.booking_repo = booking_repo

    def create_booking(self, user_id, table_id, date, time):
        if self.booking_repo.is_available(table_id, date, time):
            return self.booking_repo.create(user_id, table_id, date, time)
        else:
            return False

    def cancel_booking(self, booking_id):
        return self.booking_repo.delete(booking_id)

    def check_availability(self, table_id, date, time):
        return self.booking_repo.is_available(table_id, date, time)
