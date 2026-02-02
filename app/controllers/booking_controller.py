class BookingController:
    def __init__(self, booking_service):
        self.booking_service = booking_service

    def create(self, data):
        return self.booking_service.create_booking(data)
