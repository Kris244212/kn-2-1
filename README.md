1. Анти-патерн God Object

God Object — це анти-патерн, за якого один клас:

знає занадто багато про систему;

виконує занадто багато різних відповідальностей;

керує логікою, даними, валідацією, збереженням, логуванням тощо;

має велику кількість методів і залежностей;

тісно пов’язаний з іншими частинами системи (high coupling).

Основні проблеми:

порушення SRP (Single Responsibility Principle);

складність тестування;

важко підтримувати та розширювати;

будь-яка зміна може зламати багато функціоналу.

2. Приклад класу, який порушує SRP
public class UserManager {

    public void createUser(String name, String email) {
        // валідація
        if (name == null || email == null) {
            throw new IllegalArgumentException("Invalid data");
        }

        // збереження в БД
        System.out.println("Saving user to database");

        // логування
        System.out.println("User created: " + name);

        // відправка email
        System.out.println("Sending welcome email to " + email);
    }
}

Чому це порушує SRP?

Клас UserManager має кілька причин для зміни:

зміна правил валідації;

зміна способу збереження даних;

зміна логування;

зміна логіки відправки email.

Тобто він відповідає не за одну, а за чотири різні речі.

3. Рефакторинг для дотримання SRP

Розділимо відповідальності на окремі класи.

Валідація
public class UserValidator {
    public void validate(String name, String email) {
        if (name == null || email == null) {
            throw new IllegalArgumentException("Invalid data");
        }
    }
}

Репозиторій
public class UserRepository {
    public void save(String name, String email) {
        System.out.println("Saving user to database");
    }
}

Email сервіс
public class EmailService {
    public void sendWelcomeEmail(String email) {
        System.out.println("Sending welcome email to " + email);
    }
}

Оновлений UserManager
public class UserManager {

    private final UserValidator validator = new UserValidator();
    private final UserRepository repository = new UserRepository();
    private final EmailService emailService = new EmailService();

    public void createUser(String name, String email) {
        validator.validate(name, email);
        repository.save(name, email);
        emailService.sendWelcomeEmail(email);
    }
}

Переваги рефакторингу:

кожен клас має одну відповідальність;

легше тестувати;

легше змінювати або розширювати систему;

код читабельніший та гнучкіший.