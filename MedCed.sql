create database medCef

use medCef


CREATE TABLE Roles (
    role_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    role_name VARCHAR(15) NOT NULL
);

CREATE TABLE AuthData (
    login_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    login VARCHAR(20) NOT NULL,
    password VARCHAR(18) NOT NULL,
    role_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
);

CREATE TABLE Patients (
    patient_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    patient_surname VARCHAR(50) NOT NULL,
    patient_name VARCHAR(50) NOT NULL,
    patient_patronymic VARCHAR(50) NOT NULL,
    patient_dateOfBirth DATE NOT NULL,
    patient_phoneNumber VARCHAR(11) NOT NULL
);

CREATE TABLE Diagnoses (
    diagnosis_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    diagnosis_name VARCHAR(50) NOT NULL,
    diagnosis_description VARCHAR(MAX) NOT NULL
);

CREATE TABLE Appointments (
    appointments_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    appointments_name VARCHAR(50) NOT NULL,
    appointments_instruction VARCHAR(MAX) NOT NULL
);

CREATE TABLE Recipes (
    recipes_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    drug_name VARCHAR(50) NOT NULL,
    drug_dosage VARCHAR(MAX) NOT NULL
);

CREATE TABLE Results (
    result_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    results VARCHAR(MAX) NOT NULL
);

CREATE TABLE Visits (
    visit_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    patient_id INT NOT NULL,
    visit_date DATE NOT NULL,
    diagnosis_id INT NOT NULL,
    result_id INT NOT NULL,
    appointments_id INT NOT NULL,
    recipes_id INT NOT NULL,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (diagnosis_id) REFERENCES Diagnoses(diagnosis_id) ON DELETE CASCADE,
    FOREIGN KEY (result_id) REFERENCES Results(result_id) ON DELETE CASCADE,
    FOREIGN KEY (appointments_id) REFERENCES Appointments(appointments_id) ON DELETE CASCADE,
    FOREIGN KEY (recipes_id) REFERENCES Recipes(recipes_id) ON DELETE CASCADE
);

INSERT INTO Roles (role_name)
VALUES ('Администратор'), ('Врач');

INSERT INTO AuthData (login, password, role_id)
VALUES ('admin', 'admin123/', 1), ('doctor', 'doctor123/', 2);

INSERT INTO Patients (patient_surname, patient_name, patient_patronymic, patient_dateOfBirth, patient_phoneNumber)
VALUES ('Иванов', 'Петр', 'Сергеевич', '1990-05-15', '12345678901'),
       ('Петров', 'Иван', 'Петрович', '1985-09-20', '9876543210'),
       ('Сидоров', 'Алексей', 'Иванович', '1978-12-10', '55544433322');

INSERT INTO Diagnoses (diagnosis_name, diagnosis_description)
VALUES ('Грипп', 'Острое вирусное заболевание дыхательных путей'),
       ('Ангина', 'Острое воспалительное заболевание миндалин'),
       ('Гастрит', 'Воспаление слизистой оболочки желудка');

INSERT INTO Appointments (appointments_name, appointments_instruction)
VALUES ('Парацетамол', 'Принимать по 1 таблетке 3 раза в день после еды'),
       ('Ибупрофен', 'Принимать по 1 таблетке 2 раза в день во время еды'),
       ('Аспирин', 'Принимать по 1 таблетке утром и вечером после еды');

INSERT INTO Recipes (drug_name, drug_dosage)
VALUES ('Амоксициллин', '500 мг каждый день'),
       ('Цефтриаксон', '1 г каждые 3 дня'),
       ('Левофлоксацин', '500 мг каждый день');

INSERT INTO Results (results)
VALUES ('Анализы в норме, отклонений не выявлено'),
       ('Обнаружены незначительные отклонения в показателях крови'),
       ('Обнаружены повышенные показатели холестерина');

INSERT INTO Visits (patient_id, visit_date, diagnosis_id, result_id, appointments_id, recipes_id)
VALUES (1, '2024-01-27', 1, 1, 1, 1),
       (2, '2024-01-28', 2, 2, 2, 2),
       (3, '2024-01-29', 3, 3, 3, 3);

-- Вывод данных
SELECT * FROM Roles;
SELECT * FROM AuthData;
SELECT * FROM Patients;
SELECT * FROM Diagnoses;
SELECT * FROM Appointments;
SELECT * FROM Recipes;
SELECT * FROM Results;
SELECT * FROM Visits;