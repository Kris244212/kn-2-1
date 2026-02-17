# LSP - Liskov Substitution Principle

```mermaid
graph TB
    IShape["<<interface>><br/>IShape<br/>---<br/>Area(): int"]
    
    RectangleLSP["RectangleLSP<br/>---<br/>Width: int<br/>Height: int<br/>---<br/>Area(): int"]
    SquareLSP["SquareLSP<br/>---<br/>Size: int<br/>---<br/>Area(): int"]
    
    RectangleLSP -->|implements| IShape
    SquareLSP -->|implements| IShape
    
    style IShape fill:#e1f5ff
    style RectangleLSP fill:#c8e6c9
    style SquareLSP fill:#c8e6c9
```

## ✅ Правильна реалізація LSP

- **RectangleLSP** реалізує **IShape**
- **SquareLSP** реалізує **IShape** (НЕ наслідує Rectangle)
- Обидві класи можна використовувати як `IShape` без проблем

## ❌ Помилкова реалізація (була раніше)

```
Square : Rectangle  // ❌ Порушує LSP!
```

**Чому це помилка?**
- Square має інші властивості (Size замість Width/Height)
- Код, що очікує Rectangle, може порушитися при використанні Square
