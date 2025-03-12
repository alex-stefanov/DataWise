# DataWise – ДатаВайс

<style>
.tabs {
  display: flex;
  margin-bottom: 1em;
  border-bottom: 2px solid #ccc;
}
.tab-button {
  padding: 10px 20px;
  cursor: pointer;
  background: #eee;
  border: none;
  outline: none;
  transition: background 0.3s;
  margin-right: 5px;
  font-size: 1em;
}
.tab-button.active {
  background: #ddd;
  border-bottom: 2px solid white;
}
.tab-content {
  display: none;
  padding: 10px;
  border: 1px solid #ccc;
  border-top: none;
}
</style>

<script>
function showTab(tabId) {
  var contents = document.getElementsByClassName("tab-content");
  for (var i = 0; i < contents.length; i++) {
    contents[i].style.display = "none";
  }
  document.getElementById(tabId).style.display = "block";
  
  var buttons = document.getElementsByClassName("tab-button");
  for (var i = 0; i < buttons.length; i++) {
    buttons[i].classList.remove("active");
  }
  document.getElementById("btn-" + tabId).classList.add("active");
}
window.onload = function() {
  showTab("english");
}
</script>

<div class="tabs">
  <button id="btn-english" class="tab-button" onclick="showTab('english')">English 🇬🇧</button>
  <button id="btn-български" class="tab-button" onclick="showTab('български')">Български 🇧🇬</button>
  <button id="btn-español" class="tab-button" onclick="showTab('español')">Español 🇪🇸</button>
  <button id="btn-deutsch" class="tab-button" onclick="showTab('deutsch')">Deutsch 🇩🇪</button>
  <button id="btn-português" class="tab-button" onclick="showTab('português')">Português 🇵🇹</button>
  <button id="btn-français" class="tab-button" onclick="showTab('français')">Français 🇫🇷</button>
</div>

<div id="english" class="tab-content">
### 1. Authors & Supervisor
- **Alex Ivailov Stefanov**  
  - Address: Kazanlak, “Dobri Kehayov” St. No. 13 🏠  
  - Phone: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - School: PPMG “Nikola Obreschkov” 🎓  
  - Class: 11B  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Phone: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Position: Teacher of Informatics and Information Technologies 👩‍🏫

### 2. Project Summary & Objectives
**Goals:**  
DataWise is designed for aspiring programmers, providing a platform to enhance skills in data structures and algorithms—essential for technical interviews at top tech companies.

**Context:**  
Interviews require deep understanding of algorithms and data structures. DataWise combines a custom AI model (built entirely in Python without external libraries) with traditional educational resources for a comprehensive learning experience.

**Key Project Phases:**  
- Idea formulation 💭  
- Architectural design 🏗️  
- Ecosystem configuration ⚙️  
- Studying Artificial Intelligence 🤖  
- Building the AI model architecture  
- Learning Front-End (TypeScript, Angular) and Python 🐍  
- Creating a dataset (~10,000 examples) 📊  
- Implementing the AI model  
- Logo and design development 🎨  
- Testing, optimization, and feedback collection 🛠️

### 3. Complexity & Mathematical Challenges
- **Linear Algebra & Matrix Operations:** Essential for combining multi-dimensional data in convolution layers. ➗  
- **Activation & Differentiability:** Uses ReLU for transforming inputs, critical for effective backpropagation. 🔄  
- **Optimization & Gradient Descent:** Trained using stochastic gradient descent with adaptive learning and momentum to avoid local minima. 📉  
- **Backpropagation:** Involves computing gradients across multiple layers. 🔍  
- **Normalization & Regularization:** Techniques like batch normalization and L2 regularization ensure training stability. ⚖️

### 4. Data Set Details
- **Source & Copyright:**  
  All data are personally collected and protected under the MIT License. 🔒  
- **Size & Structure:**  
  Nearly 10,000 carefully selected examples for training and validation. 📚  
- **Categories:**  
  - BFS (Breadth-First Search)  
  - DFS (Depth-First Search)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Undefined/Other) 📑

### 5. Ecosystems & Integration
DataWise integrates:
- **Angular (Front-End):** Provides a modern, interactive UI via RESTful APIs. 💻  
- **Flask (Python):** Hosts the AI model and handles complex computations. 🐍  
- **ASP.NET:** Implements additional services and business logic. 🔌  
- **Cross-Layer Integration:** Ensures all components work in harmony. 🤝

### 6. Architecture & Components
- **DataWise.AI:** Contains the AI module (Python, custom TextCNN via Flask). 🤖  
- **DataWise.Api:** API layer (ASP.NET) connecting the AI module with the UI. 🔗  
- **DataWise.Client:** Front-end application built with Angular. 🌐  
- **DataWise.Core:** Core services and business logic using .NET. ⚙️  
- **DataWise.Data:** Manages data storage and access (relational and non-relational databases). 🗄️  
- **DataWise.Common:** Shared constants and helper functions. 🔧

### 7. Functionalities
- **Local Model Execution:** Processes input text and provides automatic categorization. 🔄  
- **Web Interface with Categorization:** Users input text and receive immediate feedback. 🌟  
- **Knowledge Nexus:** Educational module with source code, descriptions, comparisons, and more for interview preparation. 📘  
- **Data Chartizer:** Processes large datasets to generate custom charts. 📈

### 8. Implementation Details (Overview)
The AI module is based on a TextCNN architecture that converts text into numerical vectors and applies sequential operations (convolution, pooling, and a fully connected layer) to classify input data. Training is performed via backpropagation to update the model parameters. 🧠

### 9. Conclusion
DataWise offers an innovative and comprehensive solution for preparing candidates for technical interviews. By combining advanced AI algorithms with a modular, scalable architecture, the project equips users with both theoretical insights and practical skills essential for success. 🎓

### 10. License & Contact
**License:**  
This project is licensed under the [MIT License](LICENSE). 📄

**Contact:**  
- **Alex Ivailov Stefanov** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>

<div id="български" class="tab-content">
### 1. Автори и Ръководител
- **Алекс Ивайлов Стефанов**  
  - Адрес: гр. Казанлък, ул. „Добри Кехайов“ №13 🏠  
  - Телефон: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - Училище: ППМГ „Никола Обрешков“ 🎓  
  - Клас: 11б  
- **Ръководител: Здравка Стефанова Димитрова**  
  - Телефон: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Длъжност: Учител по информатика и информационни технологии 👩‍🏫

### 2. Резюме и Цели
**Цели:**  
DataWise е насочен към кандидатите в програмирането, като предоставя платформа за усъвършенстване на знанията по структури от данни и алгоритми – умения, критични за интервюта в водещи технологични компании. 💡

**Контекст:**  
Интервютата изискват задълбочено познаване на алгоритми и структури от данни. DataWise съчетава собствен AI модел (разработен изцяло на Python без външни библиотеки) с традиционни образователни ресурси. 🎯

**Основни етапи:**  
- Формулиране на идеята 💭  
- Изграждане на архитектура 🏗️  
- Конфигуриране на екосистемата ⚙️  
- Изучаване на AI 🤖  
- Изграждане на архитектурата на AI модела  
- Обучение по Front-End (TypeScript, Angular) и Python 🐍  
- Създаване на dataset (~10,000 примера) 📊  
- Имплементация на AI модела  
- Разработка на лого и дизайн 🎨  
- Тестване, оптимизация и събиране на обратна връзка 🛠️

### 3. Математически Сложности
- **Линейна алгебра и матрични операции:** Ключови за комбиниране на данни в конволюционните слоеве. ➗  
- **Функция на активация и диференциируемост:** Използва се ReLU, необходима за правилното изчисляване на градиенти. 🔄  
- **Оптимизация и градиентен спуск:** Използва се стохастичен градиентен спуск с адаптивни техники. 📉  
- **Обратна разпространение:** Изчисляване на градиенти през няколко слоя. 🔍  
- **Нормализация и регуларизация:** Batch normalization и L2-регуларизация за стабилност на обучението. ⚖️

### 4. Данни (Dataset)
- **Източник и авторски права:**  
  Всички данни са събрани и подготвени лично и са защитени с MIT лиценз. 🔒  
- **Размер и структура:**  
  Dataset съдържа близо 10,000 примера за обучение и валидиране. 📚  
- **Категории:**  
  - BFS (Обхождане в ширина)  
  - DFS (Обхождане в дълбочина)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Неопределено/Други) 📑

### 5. Екосистеми и Интеграция
- **Angular (Front-End):** Осигурява модерен и интерактивен интерфейс чрез RESTful API. 💻  
- **Flask (Python):** Изпълнява AI модела и сложните изчисления. 🐍  
- **ASP.NET:** Реализира допълнителни услуги и бизнес логика. 🔌  
- **Свързаност:** Интеграцията на всички технологии гарантира синхронна работа. 🤝

### 6. Архитектура и Компоненти
- **DataWise.AI:**  
  Съдържа AI модул, разработен на Python (TextCNN чрез Flask). 🤖  
- **DataWise.Api:**  
  API слой, базиран на ASP.NET, който свързва AI модула с интерфейса. 🔗  
- **DataWise.Client:**  
  Фронтенд, разработен с Angular, предоставящ интуитивен интерфейс. 🌐  
- **DataWise.Core:**  
  Основни услуги и бизнес логика, реализирани на .NET. ⚙️  
- **DataWise.Data:**  
  Слой за управление на данни (релационни и нерелационни бази). 🗄️  
- **DataWise.Common:**  
  Общи константи и помощни функции. 🔧

### 7. Функционалности
- **Локално изпълнение на модела:** Обработва входни данни и извършва автоматична категоризация. 🔄  
- **Уеб интерфейс с категоризация:** Потребителите въвеждат текст и получават незабавна обратна връзка. 🌟  
- **Knowledge Nexus:** Образователен модул с информация за подготовка за интервюта и изпити. 📘  
- **Data Chartizer:** Обработка на големи datasets за генериране на персонализирани диаграми. 📈

### 8. Реализация (Общо описание)
AI модулът, базиран на TextCNN архитектура, преобразува текст в числови вектори и прилага последователни операции (конволюция, пул, напълно свързан слой) за класификация. Обучението се извършва чрез backpropagation за актуализиране на параметрите. 🧠

### 9. Заключение
DataWise представлява иновативно решение за подготовка на кандидати за технически интервюта, като предоставя теоретични знания и практически умения, от съществено значение за успех. 🎓

### 10. Лиценз и Контакт
**Лиценз:**  
Проектът е лицензиран под [MIT License](LICENSE). 📄  
**Контакт:**  
- **Алекс Ивайлов Стефанов** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Ръководител: Здравка Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>

<div id="español" class="tab-content">
### 1. Autores y Supervisor
- **Alex Ivailov Stefanov**  
  - Dirección: Kazanlak, calle “Dobri Kehayov” No. 13 🏠  
  - Teléfono: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - Escuela: PPMG “Nikola Obreschkov” 🎓  
  - Clase: 11B  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Teléfono: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Cargo: Profesora de Informática y Tecnologías de la Información 👩‍🏫

### 2. Resumen y Objetivos
**Objetivos:**  
DataWise está diseñado para preparar a futuros programadores, proporcionando una plataforma para mejorar conocimientos en estructuras de datos y algoritmos, esenciales para entrevistas en grandes empresas tecnológicas. 💡

**Contexto:**  
Las entrevistas requieren un profundo entendimiento de algoritmos y estructuras de datos. DataWise combina un modelo de IA personalizado (desarrollado íntegramente en Python sin bibliotecas externas) con recursos educativos tradicionales. 🎯

**Fases del Proyecto:**  
- Formulación de la idea 💭  
- Diseño arquitectónico 🏗️  
- Configuración del ecosistema ⚙️  
- Estudio de la IA 🤖  
- Construcción de la arquitectura del modelo  
- Aprendizaje de Front-End (TypeScript, Angular) y Python 🐍  
- Creación de un dataset (~10,000 ejemplos) 📊  
- Implementación del modelo  
- Desarrollo de logotipo y diseño 🎨  
- Pruebas, optimización y retroalimentación 🛠️

### 3. Complejidades y Desafíos Matemáticos
- **Álgebra Lineal y Operaciones Matriciales:** Fundamentales para combinar datos en las capas de convolución. ➗  
- **Función de Activación y Diferenciabilidad:** Uso de ReLU, esencial para el cálculo correcto de gradientes. 🔄  
- **Optimización y Descenso de Gradiente:** Uso de SGD con técnicas adaptativas y momentum para evitar mínimos locales. 📉  
- **Backpropagation:** Cálculo y seguimiento de gradientes a través de operaciones complejas. 🔍  
- **Normalización y Regularización:** Batch normalization y L2 regularización estabilizan el entrenamiento. ⚖️

### 4. Detalles del Dataset
- **Fuente y Derechos:**  
  Los datos han sido recolectados y preparados personalmente y están protegidos bajo la licencia MIT. 🔒  
- **Tamaño y Estructura:**  
  Un dataset con casi 10,000 ejemplos para entrenamiento y validación. 📚  
- **Categorías:**  
  - BFS (Búsqueda en anchura)  
  - DFS (Búsqueda en profundidad)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Indefinido/Otros) 📑

### 5. Ecosistemas e Integración
- **Angular (Front-End):** Proporciona una interfaz moderna e interactiva a través de APIs RESTful. 💻  
- **Flask (Python):** Hospeda el modelo de IA y realiza cálculos complejos. 🐍  
- **ASP.NET:** Implementa servicios adicionales y la lógica de negocio. 🔌  
- **Integración Total:** Garantiza el funcionamiento coordinado de todos los componentes. 🤝

### 6. Arquitectura y Componentes
- **DataWise.AI:**  
  Contiene el módulo de IA desarrollado en Python (modelo TextCNN a través de Flask). 🤖  
- **DataWise.Api:**  
  Capa API basada en ASP.NET que conecta el módulo de IA con la interfaz de usuario. 🔗  
- **DataWise.Client:**  
  Aplicación front-end desarrollada con Angular. 🌐  
- **DataWise.Core:**  
  Servicios centrales y lógica de negocio implementados en .NET. ⚙️  
- **DataWise.Data:**  
  Gestión y almacenamiento de datos (bases de datos relacionales y no relacionales). 🗄️  
- **DataWise.Common:**  
  Constantes y funciones auxiliares reutilizables. 🔧

### 7. Funcionalidades
- **Ejecución Local del Modelo:** El modelo procesa el texto y clasifica automáticamente. 🔄  
- **Interfaz Web con Categorización:** Los usuarios ingresan texto y reciben retroalimentación inmediata. 🌟  
- **Knowledge Nexus:** Módulo educativo con código fuente, descripciones y comparaciones para preparación de entrevistas. 📘  
- **Data Chartizer:** Procesa grandes datasets para generar gráficos personalizados. 📈

### 8. Descripción de la Implementación (Resumen)
El módulo de IA basado en TextCNN transforma el texto en vectores numéricos y aplica operaciones secuenciales (convolución, pooling y capa completamente conectada) para clasificar la información. El entrenamiento utiliza backpropagation para ajustar los parámetros. 🧠

### 9. Conclusión
DataWise ofrece una solución innovadora y completa para preparar a los candidatos en entrevistas técnicas, combinando algoritmos avanzados y una arquitectura modular que aporta conocimientos teóricos y habilidades prácticas. 🎓

### 10. Licencia y Contacto
**Licencia:**  
Este proyecto está licenciado bajo la [MIT License](LICENSE). 📄  
**Contacto:**  
- **Alex Ivailov Stefanov** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>

<div id="deutsch" class="tab-content">
### 1. Autoren und Betreuer
- **Alex Ivailov Stefanov**  
  - Adresse: Kazanlak, „Dobri Kehayov“ Str. Nr. 13 🏠  
  - Telefon: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - Schule: PPMG „Nikola Obreschkov“ 🎓  
  - Klasse: 11B  
- **Betreuer: Zdravka Stefanova Dimitrova**  
  - Telefon: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Position: Lehrerin für Informatik und Informationstechnologien 👩‍🏫

### 2. Projektübersicht und Ziele
**Ziele:**  
DataWise richtet sich an angehende Programmierer und bietet eine Plattform zur Vertiefung der Kenntnisse in Datenstrukturen und Algorithmen – essenziell für technische Interviews bei führenden Technologieunternehmen. 💡

**Kontext:**  
Technische Interviews erfordern ein tiefes Verständnis von Algorithmen und Datenstrukturen. DataWise kombiniert ein eigens entwickeltes KI-Modell (komplett in Python ohne externe Bibliotheken) mit traditionellen Lernressourcen. 🎯

**Projektphasen:**  
- Ideenfindung 💭  
- Architekturgestaltung 🏗️  
- Konfiguration des Ökosystems ⚙️  
- Studium der KI 🤖  
- Aufbau der Modellarchitektur  
- Lernen von Front-End (TypeScript, Angular) und Python 🐍  
- Erstellung eines Datensatzes (~10.000 Beispiele) 📊  
- Implementierung des Modells  
- Logo- und Designentwicklung 🎨  
- Testen, Optimierung und Feedback 🛠️

### 3. Mathematische Herausforderungen
- **Lineare Algebra und Matrixoperationen:** Wichtig für das Training, z. B. zur Kombination von Daten in Convolution-Schichten. ➗  
- **Aktivierungsfunktion und Differenzierbarkeit:** ReLU muss differenzierbar sein für effektives Backpropagation. 🔄  
- **Optimierung und Gradientenabstieg:** Einsatz von SGD mit adaptiver Lernrate und Momentum zur Vermeidung lokaler Minima. 📉  
- **Backpropagation:** Komplexe Berechnungen der Gradienten über mehrere Schichten. 🔍  
- **Normalisierung und Regularisierung:** Batch Normalization und L2-Regularisierung stabilisieren das Training. ⚖️

### 4. Datensatzdetails
- **Quelle und Urheberrecht:**  
  Alle Daten wurden persönlich gesammelt und sind durch die MIT-Lizenz geschützt. 🔒  
- **Größe und Struktur:**  
  Ein Datensatz mit nahezu 10.000 Beispielen für Ausbildung und Validierung. 📚  
- **Kategorien:**  
  - BFS (Breitensuche)  
  - DFS (Tiefensuche)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Nicht definiert/Andere) 📑

### 5. Ökosysteme und Integration
- **Angular (Front-End):** Bietet eine moderne, interaktive Benutzeroberfläche über RESTful APIs. 💻  
- **Flask (Python):** Hoster des KI-Modells und Durchführung komplexer Berechnungen. 🐍  
- **ASP.NET:** Implementiert zusätzliche Dienste und Geschäftslogik. 🔌  
- **Integration:** Sorgt für den reibungslosen Betrieb aller Komponenten. 🤝

### 6. Architektur und Komponenten
- **DataWise.AI:**  
  Enthält das in Python entwickelte KI-Modul (TextCNN über Flask). 🤖  
- **DataWise.Api:**  
  Eine auf ASP.NET basierende API-Schicht, die das KI-Modul mit der Benutzeroberfläche verbindet. 🔗  
- **DataWise.Client:**  
  Das Front-End, entwickelt mit Angular. 🌐  
- **DataWise.Core:**  
  Zentrale Dienste und Geschäftslogik in .NET. ⚙️  
- **DataWise.Data:**  
  Verwaltung und Speicherung von Daten (relationale und nicht-relationale Datenbanken). 🗄️  
- **DataWise.Common:**  
  Gemeinsame Konstanten und Hilfsfunktionen. 🔧

### 7. Funktionalitäten
- **Lokale Ausführung des Modells:** Das Modell verarbeitet Eingabedaten und klassifiziert automatisch. 🔄  
- **Webbasierte Kategorisierung:** Benutzer geben Text ein und erhalten sofortiges Feedback. 🌟  
- **Knowledge Nexus:** Bildungsmodul mit Quellcode, Beschreibungen und Vergleichen zur Vorbereitung. 📘  
- **Data Chartizer:** Schnelle Verarbeitung großer Datensätze zur Erstellung personalisierter Diagramme. 📈

### 8. Implementierungsdetails (Kurzbeschreibung)
Das KI-Modul basiert auf einer TextCNN-Architektur, die Text in numerische Vektoren umwandelt und durch mehrere Schichten (Convolution, Pooling, Fully Connected) klassifiziert. Das Training erfolgt über Backpropagation zur Anpassung der Modellparameter. 🧠

### 9. Fazit
DataWise demonstriert hohes technisches Potenzial und bietet eine umfassende Lösung zur Vorbereitung auf technische Interviews. Die Kombination aus fortschrittlichen Algorithmen und einer modularen Architektur vermittelt sowohl theoretisches Wissen als auch praktische Fähigkeiten. 🎓

### 10. Lizenz und Kontakt
**Lizenz:**  
Dieses Projekt steht unter der [MIT License](LICENSE). 📄  
**Kontakt:**  
- **Alex Ivailov Stefanov** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Betreuerin: Zdravka Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>

<div id="português" class="tab-content">
### 1. Autores e Supervisor
- **Alex Ivailov Stefanov**  
  - Endereço: Kazanlak, “Dobri Kehayov” St. Nº 13 🏠  
  - Telefone: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - Escola: PPMG “Nikola Obreschkov” 🎓  
  - Turma: 11B  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Telefone: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Cargo: Professora de Informática e Tecnologias da Informação 👩‍🏫

### 2. Resumo e Objetivos
**Objetivos:**  
DataWise foi criado para preparar futuros programadores, oferecendo uma plataforma para aprimorar conhecimentos em estruturas de dados e algoritmos – habilidades essenciais para entrevistas em grandes empresas de tecnologia. 💡

**Contexto:**  
Entrevistas técnicas exigem um entendimento profundo de algoritmos e estruturas de dados. DataWise combina um modelo de IA personalizado (desenvolvido inteiramente em Python sem bibliotecas externas) com recursos educacionais tradicionais. 🎯

**Fases do Projeto:**  
- Formulação da ideia 💭  
- Design arquitetônico 🏗️  
- Configuração do ecossistema ⚙️  
- Estudo de IA 🤖  
- Construção da arquitetura do modelo  
- Aprendizado de Front-End (TypeScript, Angular) e Python 🐍  
- Criação de um dataset (~10,000 exemplos) 📊  
- Implementação do modelo  
- Desenvolvimento do logotipo e design 🎨  
- Testes, otimização e coleta de feedback 🛠️

### 3. Desafios Matemáticos
- **Álgebra Linear e Operações Matriciais:** Fundamentais para combinar dados em camadas de convolução. ➗  
- **Função de Ativação e Diferenciabilidade:** ReLU deve ser diferenciável para um backpropagation eficaz. 🔄  
- **Otimização e Gradiente Descendente:** Uso de SGD com técnicas adaptativas e momentum para evitar mínimos locais. 📉  
- **Backpropagation:** Cálculo e propagação de gradientes através de operações não lineares. 🔍  
- **Normalização e Regularização:** Batch normalization e L2 regularização garantem a estabilidade do treinamento. ⚖️

### 4. Detalhes do Dataset
- **Fonte e Direitos:**  
  Todos os dados foram coletados e preparados pessoalmente e estão protegidos pela licença MIT. 🔒  
- **Tamanho e Estrutura:**  
  Um dataset com cerca de 10,000 exemplos para treinamento e validação. 📚  
- **Categorias:**  
  - BFS (Busca em Largura)  
  - DFS (Busca em Profundidade)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Indefinido/Outros) 📑

### 5. Ecossistemas e Integração
- **Angular (Front-End):** Oferece uma interface moderna via APIs RESTful. 💻  
- **Flask (Python):** Hospeda o modelo de IA e realiza cálculos complexos. 🐍  
- **ASP.NET:** Implementa serviços adicionais e lógica de negócio. 🔌  
- **Integração:** Todos os componentes funcionam de forma coordenada. 🤝

### 6. Arquitetura e Componentes
- **DataWise.AI:**  
  Contém o módulo de IA desenvolvido em Python (modelo TextCNN via Flask). 🤖  
- **DataWise.Api:**  
  Camada de API baseada em ASP.NET que conecta o módulo de IA à interface do usuário. 🔗  
- **DataWise.Client:**  
  Aplicação front-end desenvolvida com Angular. 🌐  
- **DataWise.Core:**  
  Serviços centrais e lógica de negócio implementados em .NET. ⚙️  
- **DataWise.Data:**  
  Gerenciamento e armazenamento de dados (bancos relacionais e não relacionais). 🗄️  
- **DataWise.Common:**  
  Constantes e funções auxiliares reutilizáveis. 🔧

### 7. Funcionalidades
- **Execução Local do Modelo:** O modelo processa os dados e realiza classificação automática. 🔄  
- **Interface Web com Categorização:** Usuários inserem texto e recebem feedback imediato. 🌟  
- **Knowledge Nexus:** Módulo educativo com código, descrições e comparações para preparação. 📘  
- **Data Chartizer:** Processa grandes datasets para gerar gráficos personalizados. 📈

### 8. Detalhes da Implementação (Resumo)
O módulo de IA baseado em TextCNN transforma o texto em vetores numéricos e aplica operações sequenciais (convolução, pooling e uma camada totalmente conectada) para classificar os dados. O treinamento utiliza backpropagation para atualizar os parâmetros do modelo. 🧠

### 9. Conclusão
DataWise representa uma solução inovadora e abrangente para preparar candidatos para entrevistas técnicas, combinando algoritmos avançados com uma arquitetura modular que oferece tanto conhecimento teórico quanto habilidades práticas. 🎓

### 10. Licença e Contato
**Licença:**  
Este projeto está licenciado sob a [MIT License](LICENSE). 📄  
**Contato:**  
- **Alex Ivailov Stefanov** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>

<div id="français" class="tab-content">
### 1. Auteurs et Superviseur
- **Alex Ivailov Stefanov**  
  - Adresse: Kazanlak, rue “Dobri Kehayov” No. 13 🏠  
  - Téléphone: 0889475177 📞  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com) ✉️  
  - École: PPMG “Nikola Obreschkov” 🎓  
  - Classe: 11B  
- **Superviseur: Zdravka Stefanova Dimitrova**  
  - Téléphone: 0893422519 📞  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) ✉️  
  - Poste: Enseignante en Informatique et Technologies de l'Information 👩‍🏫

### 2. Résumé et Objectifs
**Objectifs :**  
DataWise vise à préparer les futurs programmeurs en offrant une plateforme pour approfondir leurs connaissances en structures de données et algorithmes – compétences essentielles pour les entretiens techniques dans les grandes entreprises technologiques. 💡

**Contexte :**  
Les entretiens techniques requièrent une compréhension approfondie des algorithmes et des structures de données. DataWise combine un modèle d'IA personnalisé (développé intégralement en Python sans bibliothèques externes) avec des ressources éducatives traditionnelles. 🎯

**Phases du Projet :**  
- Formulation de l'idée 💭  
- Conception architecturale 🏗️  
- Configuration de l'écosystème ⚙️  
- Étude de l'IA 🤖  
- Construction de l'architecture du modèle  
- Apprentissage du Front-End (TypeScript, Angular) et de Python 🐍  
- Création d'un dataset (~10 000 exemples) 📊  
- Implémentation du modèle  
- Développement du logo et du design 🎨  
- Tests, optimisation et collecte de feedback 🛠️

### 3. Défis Mathématiques
- **Algèbre Linéaire et Opérations Matricielles :** Essentielles pour l'entraînement, par exemple pour combiner les données dans les couches de convolution. ➗  
- **Fonction d'Activation et Différentiabilité :** L'utilisation de ReLU nécessite qu'elle soit différentiable pour une rétropropagation efficace. 🔄  
- **Optimisation et Descente de Gradient :** Utilisation du SGD avec des techniques adaptatives et du momentum pour éviter les minima locaux. 📉  
- **Backpropagation :** Calcul et propagation des gradients à travers plusieurs couches. 🔍  
- **Normalisation et Régularisation :** Batch normalization et régularisation L2 assurent la stabilité de l'entraînement. ⚖️

### 4. Détails du Dataset
- **Source et Droits :**  
  Toutes les données ont été collectées et préparées personnellement et sont protégées par la licence MIT. 🔒  
- **Taille et Structure :**  
  Un dataset comprenant près de 10 000 exemples pour l'entraînement et la validation. 📚  
- **Catégories :**  
  - BFS (Recherche en largeur)  
  - DFS (Recherche en profondeur)  
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Indéfini/Autres) 📑

### 5. Écosystèmes et Intégration
- **Angular (Front-End) :** Fournit une interface moderne et interactive via des APIs RESTful. 💻  
- **Flask (Python) :** Héberge le modèle d'IA et effectue des calculs complexes. 🐍  
- **ASP.NET :** Implémente des services additionnels et la logique métier. 🔌  
- **Intégration Globale :** Assure le fonctionnement synchronisé de tous les composants. 🤝

### 6. Architecture et Composants
- **DataWise.AI :**  
  Contient le module d'IA développé en Python (modèle TextCNN accessible via Flask). 🤖  
- **DataWise.Api :**  
  Une couche API basée sur ASP.NET reliant le module d'IA à l'interface utilisateur. 🔗  
- **DataWise.Client :**  
  L'application front-end développée en Angular. 🌐  
- **DataWise.Core :**  
  Fournit les services centraux et la logique métier en .NET. ⚙️  
- **DataWise.Data :**  
  Gère le stockage et l'accès aux données (bases de données relationnelles et non relationnelles). 🗄️  
- **DataWise.Common :**  
  Constantes communes et fonctions utilitaires réutilisables. 🔧

### 7. Fonctionnalités
- **Exécution Locale du Modèle :** Le modèle traite les données d'entrée et effectue une classification automatique. 🔄  
- **Interface Web avec Catégorisation :** Les utilisateurs saisissent du texte et reçoivent un retour immédiat. 🌟  
- **Knowledge Nexus :** Module éducatif fournissant code, descriptions et comparaisons pour la préparation aux entretiens. 📘  
- **Data Chartizer :** Permet de traiter rapidement de grands datasets pour générer des graphiques personnalisés. 📈

### 8. Description de l'Implémentation (Résumé)
Le module d'IA, basé sur une architecture TextCNN, transforme le texte en vecteurs numériques et applique des opérations séquentielles (convolution, pooling, couche entièrement connectée) pour classifier les données. L'entraînement se fait par rétropropagation pour ajuster les paramètres. 🧠

### 9. Conclusion
DataWise offre une solution innovante et complète pour préparer les candidats aux entretiens techniques. La combinaison d'algorithmes avancés et d'une architecture modulaire permet d'acquérir des connaissances théoriques et des compétences pratiques essentielles. 🎓

### 10. Licence et Contact
**Licence :**  
Ce projet est sous licence [MIT License](LICENSE). 📄  
**Contact :**  
- **Alex Ivailov Stefanov** – [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Superviseure : Zdravka Stefanova Dimitrova** – [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com) 📬
</div>
