Принципи ISP та DIP
1. Interface Segregation Principle (ISP)

Принцип розділення інтерфейсів (ISP) стверджує, що клієнти не повинні залежати від інтерфейсів, які вони не використовують. Іншими словами, краще мати кілька «вузьких» спеціалізованих інтерфейсів, ніж один великий «жирний» інтерфейс.

Приклад порушення ISP:

interface Worker {
    void work();
    void eat();
}

class HumanWorker implements Worker {
    public void work() { /* робота */ }
    public void eat() { /* їжа */ }
}

class RobotWorker implements Worker {
    public void work() { /* робота */ }
    public void eat() { throw new UnsupportedOperationException(); }
}


У цьому прикладі RobotWorker змушений реалізовувати метод eat(), який йому не потрібен. Це порушує ISP.

Рішення:

interface Workable {
    void work();
}

interface Eatable {
    void eat();
}

class HumanWorker implements Workable, Eatable {
    public void work() { /* робота */ }
    public void eat() { /* їжа */ }
}

class RobotWorker implements Workable {
    public void work() { /* робота */ }
}


Тепер кожен клас реалізує лише ті інтерфейси, які йому потрібні.

2. Dependency Inversion Principle (DIP)

Принцип інверсії залежностей (DIP) говорить, що високорівневі модулі не повинні залежати від низькорівневих; обидва повинні залежати від абстракцій (інтерфейсів). Це часто реалізується через Dependency Injection (DI).

Приклад з DI:

interface MessageService {
    void sendMessage(String message);
}

class EmailService implements MessageService {
    public void sendMessage(String message) { /* відправка емейлу */ }
}

class Notification {
    private final MessageService service;

    // DI через конструктор
    public Notification(MessageService service) {
        this.service = service;
    }

    public void alert(String msg) {
        service.sendMessage(msg);
    }
}

// Використання
MessageService emailService = new EmailService();
Notification notification = new Notification(emailService);


Переваги DIP через DI:

Гнучкість: Легко замінити реалізацію без зміни клієнта.

Тестування: Можна підставити mock-об’єкти для unit-тестів.

Модульність: Зменшує зв’язність між класами.

3. Взаємозв’язок ISP та DI

«Вузькі» інтерфейси (ISP) дозволяють вводити залежності більш точно, без непотрібних методів.

Це робить DI більш чистим і безпечним: клас отримує тільки те, що йому дійсно потрібно.

У тестуванні можна легко підміняти окремі частини системи, не турбуючись про невикористані методи.