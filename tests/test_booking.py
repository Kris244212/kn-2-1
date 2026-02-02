from app.services.booking_service import BookingService

class FakeRepo:
    def is_available(self, table_id, date, time):
        return True

    def create(self, user_id, table_id, date, time):
        return True


def test_create_booking():
    service = BookingService(FakeRepo())
    result = service.create_booking(1, 1, "2026-02-01", "18:00")
    assert result == True
