from app.models.table import Table


class TableRepository:
    def list_tables(self):
        return [Table(id=1, number=1, seats=4)]
