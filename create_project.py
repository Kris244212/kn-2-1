from pathlib import Path

# Назва головної папки проєкту
project_name = "antivirus_coursework"

# Список папок
folders = [
    project_name,
    f"{project_name}/antivirus",
    f"{project_name}/quarantine",
    f"{project_name}/reports"
]

# Словник файлів і їх початкового вмісту
files = {
    f"{project_name}/main.py": "",
    f"{project_name}/signatures.txt": """eval(base64.b64decode(
powershell -enc
cmd.exe /c
CreateRemoteThread
VirtualAlloc
WScript.Shell
Shell.Application
reg add HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run
Invoke-Expression
Start-Process
""",
    f"{project_name}/blocked_hashes.txt": "",
    f"{project_name}/trusted_hashes.txt": "",
    f"{project_name}/file_integrity.json": "{}",
    f"{project_name}/antivirus/__init__.py": "",
    f"{project_name}/antivirus/logger.py": "",
    f"{project_name}/antivirus/strategies.py": "",
    f"{project_name}/antivirus/factory.py": "",
    f"{project_name}/antivirus/scanner.py": "",
    f"{project_name}/antivirus/report_manager.py": "",
    f"{project_name}/antivirus/integrity_checker.py": "",
    f"{project_name}/antivirus/security_monitor.py": "",
    f"{project_name}/antivirus/statistics_manager.py": "",
}

# Створення папок
for folder in folders:
    Path(folder).mkdir(parents=True, exist_ok=True)

# Створення файлів
for file_path, content in files.items():
    path = Path(file_path)
    if not path.exists():
        path.write_text(content, encoding="utf-8")

print("Структуру проєкту успішно створено!")