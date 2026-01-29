# Lab22 - Class Diagram (Liskov Substitution Principle)

## Діаграма класів

```mermaid
classDiagram
    class IShape {
        <<interface>>
        +Area() int
    }

    class Rectangle {
        -width: int
        -height: int
        +Width: int
        +Height: int
        +Area() int
    }

    class Square {
        -size: int
        +Size: int
        +Area() int
    }

    class RectangleLSP {
        -width: int
        -height: int
        +Width: int
        +Height: int
        +Area() int
    }

    class SquareLSP {
        -size: int
        +Size: int
        +Area() int
    }

    class ShapeRecord {
        -id: int
        -shapeType: string
        -dimension1: int
        -dimension2: int?
        -calculatedArea: int
        -createdAt: DateTime
        +Id: int
        +ShapeType: string
        +Dimension1: int
        +Dimension2: int?
        +CalculatedArea: int
        +CreatedAt: DateTime
        +ToString() string
    }

    class ShapeDbContext {
        -shapes: DbSet~ShapeRecord~
        +OnConfiguring(DbContextOptionsBuilder)
        +OnModelCreating(ModelBuilder)
    }

    Rectangle --|> IShape: implements
    Square --|> IShape: implements
    RectangleLSP --|> IShape: implements
    SquareLSP --|> IShape: implements
    
    ShapeDbContext --* ShapeRecord: contains
    
    style IShape fill:#e1f5ff,stroke:#0277bd,stroke-width:2px
    style Rectangle fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    style Square fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    style RectangleLSP fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px
    style SquareLSP fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px
    style ShapeRecord fill:#e8f5e9,stroke:#388e3c,stroke-width:2px
    style ShapeDbContext fill:#fce4ec,stroke:#c2185b,stroke-width:2px
```

## 📋 Легенда

| Клас | Опис |
|------|------|
| **IShape** | Інтерфейс, який визначає контракт для всіх фігур |
| **Rectangle** | Прямокутник (помилкова реалізація) |
| **Square** | Квадрат - успадковується від Rectangle ❌ |
| **RectangleLSP** | Прямокутник - реалізує IShape ✅ |
| **SquareLSP** | Квадрат - реалізує IShape ✅ |
| **ShapeRecord** | Модель даних для БД |
| **ShapeDbContext** | Entity Framework контекст |

## ✅ Правильна реалізація (LSP)

```
RectangleLSP ─→ IShape
SquareLSP ────→ IShape
```

- Обидва класи реалізують IShape окремо
- Можна використовувати як IShape без проблем
- Немає спадкування між ними

## ❌ Помилкова реалізація

```
Rectangle ─→ IShape
Square ────→ Rectangle  ← ПОМИЛКА! Порушує LSP
```

- Square успадковується від Rectangle
- Має інші властивості (Size замість Width/Height)
- Код очікує Rectangle, але отримує Square

## 🗄️ База даних (SQLite)

**Файл:** `shapes.db`

**Таблиця:** `Shapes`

```
ShapeRecord {
  Id: int (Primary Key)
  ShapeType: string ("Rectangle" | "Square")
  Dimension1: int (Width або Size)
  Dimension2: int? (Height, тільки для Rectangle)
  CalculatedArea: int
  CreatedAt: DateTime
}
```

## 📂 Структура проекту

```
Lab22/
├── lab22/
│   ├── Program.cs
│   ├── Shapes/
│   │   ├── IShape.cs
│   │   ├── Rectangle.cs
│   │   ├── Square.cs
│   │   ├── RectangleLSP.cs
│   │   └── SquareLSP.cs
│   ├── Models/
│   │   └── ShapeRecord.cs
│   ├── Data/
│   │   └── ShapeDbContext.cs
│   └── lab22.csproj
├── class-diagram.html
├── diagram.html
└── diagram.md
```

## 🚀 Як запустити

```bash
cd Lab22/lab22
dotnet run
```

**Результат:**
- Обчислює площі фігур
- Зберігає в базу даних SQLite
- Виводить дані з БД

## 🔗 Зв'язки в БД

Кожна фігура зберігається як `ShapeRecord`:
- Rectangle (5, 10) → Area = 50
- Square (7) → Area = 49

**Всього фігур:** 2
**Сумарна площа:** 99
