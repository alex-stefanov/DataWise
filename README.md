# DataWise – ДатаВайс

**Theme:**  
Application for facilitating acquaintance and deepening knowledge in the field of data structures and algorithms.

**Supported Languages:**  
- English (default)  
- Български  
- Español  
- Deutsch  
- Português  
- Français

---

## Table of Contents

- [English](#english)
- [Български](#български)
- [Español](#español)
- [Deutsch](#deutsch)
- [Português](#português)
- [Français](#français)

---

## English

### 1. Authors & Supervisor

- **Alex Ivailov Stefanov**  
  - Address: Kazanlak, “Dobri Kehayov” St. No. 13  
  - Phone: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - School: PPMG “Nikola Obreschkov”  
  - Class: 11B  

- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Phone: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Position: Teacher of Informatics and Information Technologies

---

### 2. Project Summary & Objectives

**Goals:**  
DataWise targets aspiring programmers by providing a platform to enhance skills in data structures and algorithms—key for technical interviews at top tech companies (e.g., Facebook, Amazon, Netflix, Google).

**Context:**  
Many technical interviews require deep understanding of algorithms and data structures. DataWise combines a custom AI model (built entirely in Python from scratch, without using external AI libraries) with traditional educational resources to provide a comprehensive learning experience.

**Key Project Phases:**
- Idea formulation  
- Architectural design  
- Ecosystem configuration  
- Studying Artificial Intelligence  
- Building the AI model architecture  
- Learning Front-End (TypeScript, Angular) and Python  
- Creating a dataset (~10,000 examples)  
- Implementing the AI model  
- Logo and overall design development  
- Testing, optimization, and feedback collection

---

### 3. Complexity & Mathematical Challenges

**Mathematical Complexities:**
- **Linear Algebra & Matrix Operations:**  
  Essential for training the AI model—operations like matrix multiplication, transposition, and inversion. For instance, tensor dot products in convolution layers combine multi-dimensional data for correct activation computations.
  
- **Activation Function & Differentiability:**  
  The model uses the ReLU (Rectified Linear Unit) activation. It transforms inputs and must be differentiable for effective backpropagation (calculating derivatives across multiple points, handling boundary cases).

- **Optimization & Gradient Descent:**  
  The model uses stochastic gradient descent (SGD) with adaptive learning rate techniques and momentum to avoid local minima, continuously updating weights by computing gradients.

- **Backpropagation:**  
  Involves computing gradients for each parameter through complex chains of differentiation (especially through convolutional layers and max-pooling operations).

- **Normalization & Regularization:**  
  Techniques such as batch normalization and L2 regularization are incorporated into the loss function and optimization process to ensure training stability.

---

### 4. Data Set Details

- **Source & Copyright:**  
  All data have been personally collected and prepared; they are protected under the MIT License.
  
- **Size & Structure:**  
  A dataset of nearly 10,000 carefully selected examples is used for training and validating the AI model.
  
- **Categories:**  
  The data are divided into 10 categories:
  - BFS (Breadth-First Search)
  - DFS (Depth-First Search)
  - Two Pointers
  - Dynamic Programming
  - Greedy Algorithm
  - Backtracking
  - Binary Search
  - Disjoint Set
  - Game Theory
  - N/A (Undefined/Other)

---

### 5. Ecosystems & Integration

DataWise integrates multiple technologies to work in unison:
- **Angular (Front-End):**  
  Provides a modern, interactive user interface that communicates with backend services via RESTful APIs.
- **Flask (Python):**  
  Hosts the AI model and handles complex mathematical computations (text processing, convolutions, gradient calculations).
- **ASP.NET:**  
  Implements additional services and business logic via standardized API endpoints, ensuring scalability and stability.
- **Cross-Layer Integration:**  
  The project coordinates Angular, Flask, and ASP.NET to work synchronously, handling diverse programming languages and frameworks.

---

### 6. Architecture & Components

The solution is modular and divided into the following key components:

- **DataWise.AI:**  
  Contains the AI module built in Python, implementing a custom TextCNN model for text classification via Flask endpoints.
  
- **DataWise.Api:**  
  An API layer based on ASP.NET that connects the AI module with the front-end, providing a scalable interface.
  
- **DataWise.Client:**  
  The front-end application developed in Angular, delivering an intuitive user interface for interacting with the system.
  
- **DataWise.Core:**  
  Provides core services and business logic using .NET for server-side operations and component integration.
  
- **DataWise.Data:**  
  Manages data storage and access (both relational and non-relational databases), ensuring reliable data management.
  
- **DataWise.Common:**  
  Contains shared constants and reusable helper functions.

---

### 7. Functionalities

**Current Functionalities:**
- **Local Model Execution:**  
  The AI model can run locally, processing input text and providing automatic categorization based on pre-trained classes.
  
- **Web Interface with Categorization:**  
  Users can input text via the website and receive immediate feedback from the AI system.
  
- **Knowledge Nexus:**  
  An educational module offering source code, descriptions, comparisons, complexity details, and other information to help users prepare for interviews or exams.
  
- **Data Chartizer:**  
  Allows users to upload large datasets and quickly extract columns for generating customized aggregated charts using optimal algorithms (e.g., similar to datasets from kaggle.com).

---

### 8. Implementation Details

#### AI Module – TextCNN Architecture

The AI module processes text by converting it into numerical vectors and applying sequential mathematical operations.

1. **Input Parameters & Initialization:**
   - **vocab_size:** Number of unique words.
   - **embedding_dim:** Dimension of each word vector.
   - **max_len:** Maximum number of words processed in an input sentence.
   - **num_filters:** Number of convolutional filters per filter size.
   - **filter_sizes:** List of filter sizes (e.g., 2, 3, 4) determining how many consecutive words are examined.
   - **num_classes:** Number of categories for classification.
   - **learning_rate:** Step size for parameter updates.

2. **Embedding Layer:**
   - Converts word indices into continuous vectors.
   - **Dimensions:**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Input sentence:  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \quad \Rightarrow \quad X_{\text{embedded}} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$
   - *Challenge:* Representing discrete text in a meaningful dense space.

3. **Convolutional Layers:**
   - For each filter size \( f \):
     - **Filter Initialization:**  
       $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
     - **Convolution Operation:**  
       $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$
     - *Challenge:* Extracting local features from variable-length text.

4. **Max Pooling & Concatenation:**
   - **Max Pooling:** For each filter \( k \) and filter size \( f \):
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Concatenation:**  
     $$ p = [p(f_1);\, p(f_2);\, \dots; \, p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$
   - *Challenge:* Combining the strongest features from multiple filter sizes.

5. **Fully Connected Layer:**
   - Converts the concatenated vector to output logits:
     - **Weights:**  
       $$ W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}}, \quad \text{where } \text{fc\_input\_dim} = \text{num\_filters} \times m $$
     - **Logits:**  
       $$ \text{logits} = p \cdot W_{fc} + b_{fc} $$
     - **Softmax:**  
       $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$
   - *Challenge:* Ensuring stable gradient flow through the softmax normalization.

6. **Backpropagation:**
   - **Gradient Calculation:**  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - **Parameter Update:**  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$
   - *Challenge:* Accurately tracking gradients through nonlinear operations like ReLU and max pooling.

---

### 9. Conclusion

DataWise offers an innovative and comprehensive solution for preparing candidates for technical interviews. By integrating advanced AI algorithms with a modular, scalable architecture, the project equips users with both deep theoretical insights and practical skills essential for success in programming careers.

---

### 10. License & Contact

**License:**  
This project is licensed under the [MIT License](LICENSE).

**Contact Information:**  
- **Alex Ivailov Stefanov**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)

---

## Български

### 1. Автори и Ръководител

- **Алекс Ивайлов Стефанов**  
  - Адрес: гр. Казанлък, ул. „Добри Кехайов“ №13  
  - Телефон: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - Училище: ППМГ „Никола Обрешков“  
  - Клас: 11б  

- **Ръководител: Здравка Стефанова Димитрова**  
  - Телефон: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Длъжност: Учител по информатика и информационни технологии

---

### 2. Резюме и Цели

**Цели:**  
DataWise е насочен към кандидатите в програмирането, като им предоставя платформа за усъвършенстване на знанията по структури от данни и алгоритми – умения, необходими за интервюта в големите технологични компании (напр. Facebook, Amazon, Netflix, Google).

**Контекст:**  
Интервютата изискват задълбочено познаване на алгоритми и структури от данни. DataWise съчетава собствен AI модел (разработен изцяло на Python без външни библиотеки) с традиционни образователни ресурси, предоставяйки цялостно обучение.

**Основни етапи:**  
- Формулиране на идеята  
- Изграждане на архитектура и конфигуриране на екосистемата  
- Изучаване на AI  
- Изграждане на архитектурата на AI модела  
- Обучение по Front-End (TypeScript, Angular) и Python  
- Създаване на dataset (~10,000 примера)  
- Имплементация на AI модела  
- Разработка на лого и дизайн  
- Тестване, оптимизация и събиране на обратна връзка

---

### 3. Математически Сложности

**Основни математически предизвикателства:**
- **Линейна алгебра и матрични операции:**  
  Операции като умножение на матрици, транспониране и инверсии са ключови за обучението на AI модела.
  
- **Функция на активация и диференциируемост:**  
  Използва се функцията ReLU, която трябва да бъде диференцируема за успешното прилагане на backpropagation.
  
- **Оптимизация и градиентен спуск:**  
  Стохастичният градиентен спуск (SGD) с адаптивно регулиране на learning rate и momentum за избягване на локални минимуми.
  
- **Обратна разпространение:**  
  Изчисляване и проследяване на градиенти през сложни нелинейни операции (напр. ReLU, max pooling).
  
- **Нормализация и регуларизация:**  
  Използват се техники като batch normalization и L2-регуларизация за стабилност на обучението.

---

### 4. Данни (Data Set)

- **Източник и авторски права:**  
  Всички данни са събрани и подготвени лично от автора и са защитени с MIT лиценз.
  
- **Размер и структура:**  
  Dataset съдържа близо 10,000 примера, подбрани за обучение и валидиране.
  
- **Категории:**  
  Данните са разделени на 10 категории:  
  - BFS (Обхождане в ширина)  
  - DFS (Обхождане в дълбочина)  
  - Two Pointers (Два указателя)  
  - Dynamic Programming (Динамично програмиране)  
  - Greedy Algorithm (Жадни алгоритми)  
  - Backtracking (Обратна проследяемост)  
  - Binary Search (Двоично търсене)  
  - Disjoint Set (Несъвместими множества)  
  - Game Theory (Теория на игрите)  
  - N/A (Неопределено/Други)

---

### 5. Екосистеми и Интеграция

- **Angular (Front-End):**  
  Осигурява модерен, интерактивен потребителски интерфейс, който комуникира чрез RESTful API.
  
- **Flask (Python):**  
  Изпълнява AI модела и сложните математически изчисления (конволюция, обработка на текст).
  
- **ASP.NET:**  
  Реализира допълнителни услуги и бизнес логика чрез стандартизирани API endpoint-и.
  
- **Свързаност:**  
  Интеграцията на Angular, Flask и ASP.NET осигурява синхронна работа на различните технологични слоеве.

---

### 6. Архитектура и Компоненти

- **DataWise.AI:**  
  Съдържа AI модула, разработен на Python с модел TextCNN, достъпен чрез Flask endpoint-и.
  
- **DataWise.Api:**  
  API слой, базиран на ASP.NET, който свързва AI модула с потребителския интерфейс.
  
- **DataWise.Client:**  
  Фронтенд, разработен с Angular, предоставящ интуитивен потребителски интерфейс.
  
- **DataWise.Core:**  
  Основни услуги и бизнес логика, реализирани на .NET.
  
- **DataWise.Data:**  
  Слой за съхранение и управление на данни (релационни и нерелационни бази).
  
- **DataWise.Common:**  
  Общи константи и преизползваеми помощни функции.

---

### 7. Функционалности

**Сегашни функционалности:**
- **Локално изпълнение на модела:**  
  Моделът обработва входни данни и извършва автоматична категоризация.
  
- **Уеб интерфейс с категоризация:**  
  Потребителите въвеждат текст и получават незабавна обратна връзка.
  
- **Knowledge Nexus:**  
  Образователна част с source код, описание, разлики, сложност и допълнителна информация за подготовка за интервюта или изпити.
  
- **Data Chartizer:**  
  Обработка на голям dataset за генериране на персонализирани диаграми чрез оптимални алгоритми.

---

### 8. Реализация – Архитектура на AI Модула (TextCNN)

1. **Входни параметри и инициализация:**
   - **vocab_size:** Брой уникални думи.  
   - **embedding_dim:** Размер на векторното представяне на думите.  
   - **max_len:** Максимален брой думи в изречение.  
   - **num_filters:** Брой конволюционни филтри за всеки размер.  
   - **filter_sizes:** Списък (напр. 2, 3, 4) – брой думи за конволюция.  
   - **num_classes:** Брой категории.  
   - **learning_rate:** Скорост на обучение.

2. **Ембединг слой:**
   - Преобразува текстови индекси в непрекъснати вектори.  
   - **Размери:**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Преобразуване на вход:  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \Rightarrow X_{embedded} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$
     
3. **Конволюционни слоеве:**
   - За всеки филтър с размер \( f \):  
     $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
   - Конволюция:  
     $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$
     
4. **Макс пул и Конкатенация:**
   - **Макс пул:**  
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Конкатенация:**  
     $$ p = [p(f_1); p(f_2); \dots; p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$
     
5. **Финален (Напълно свързан) слой:**
   - Преобразува конкатенирания вектор към логити:  
     $$ \text{logits} = p \cdot W_{fc} + b_{fc}, \quad W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}} $$
   - **Softmax:**  
     $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$
     
6. **Обратна разпространение:**
   - Изчисляване на градиентите:  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - Актуализация на параметрите:  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$

---

### 9. Заключение

DataWise представлява цялостно и иновативно решение за подготовка на кандидати за технически интервюта. Чрез интеграцията на сложни математически алгоритми и модулна архитектура, проектът предоставя задълбочени теоретични знания и практически умения, които са от съществено значение за успех в областта на програмирането.

---

### 10. Лиценз и Контакт

**Лиценз:**  
Проектът е лицензиран под [MIT License](LICENSE).

**Контакт:**  
- **Алекс Ивайлов Стефанов**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Ръководител: Здравка Стефанова Димитрова**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)

---

## Español

### 1. Autores y Supervisor

- **Alex Ivailov Stefanov**  
  - Dirección: Kazanlak, calle “Dobri Kehayov” No. 13  
  - Teléfono: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - Escuela: PPMG “Nikola Obreschkov”  
  - Clase: 11B  

- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Teléfono: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Cargo: Profesora de Informática y Tecnologías de la Información

---

### 2. Resumen y Objetivos

**Objetivos:**  
DataWise está diseñado para preparar a futuros programadores, proporcionando una plataforma para mejorar los conocimientos en estructuras de datos y algoritmos, esenciales para entrevistas en grandes empresas tecnológicas (e.g., Facebook, Amazon, Netflix, Google).

**Contexto:**  
Las entrevistas requieren un profundo entendimiento de algoritmos y estructuras de datos. DataWise combina un modelo de IA personalizado (desarrollado completamente en Python sin bibliotecas externas) con recursos educativos tradicionales.

**Fases del Proyecto:**
- Formulación de la idea  
- Diseño arquitectónico  
- Configuración del ecosistema  
- Estudio del AI  
- Construcción de la arquitectura del modelo  
- Aprendizaje de Front-End (TypeScript, Angular) y Python  
- Creación de un dataset (~10,000 ejemplos)  
- Implementación del modelo  
- Diseño y desarrollo visual  
- Pruebas, optimización y retroalimentación

---

### 3. Complejidades y Desafíos Matemáticos

- **Álgebra Lineal y Operaciones Matriciales:**  
  Operaciones como la multiplicación, transposición e inversión de matrices son cruciales para el entrenamiento del modelo.
  
- **Función de Activación y Diferenciabilidad:**  
  Se utiliza la función ReLU, que debe ser diferenciable para el cálculo correcto de gradientes en backpropagation.
  
- **Optimización y Descenso de Gradiente:**  
  Uso de descenso de gradiente estocástico (SGD) con técnicas adaptativas y momentum para evitar mínimos locales.
  
- **Backpropagation:**  
  Cálculo y seguimiento de gradientes a través de operaciones no lineales.
  
- **Normalización y Regularización:**  
  Se aplican técnicas como batch normalization y L2-regularización para estabilizar el entrenamiento.

---

### 4. Detalles del Dataset

- **Fuente y Derechos:**  
  Los datos han sido recolectados y preparados personalmente y están protegidos bajo licencia MIT.
  
- **Tamaño y Estructura:**  
  Un dataset con casi 10,000 ejemplos para entrenamiento y validación.
  
- **Categorías:**  
  Los datos se dividen en 10 categorías:
  - BFS (Búsqueda en anchura)
  - DFS (Búsqueda en profundidad)
  - Two Pointers
  - Dynamic Programming
  - Greedy Algorithm
  - Backtracking
  - Binary Search
  - Disjoint Set
  - Game Theory
  - N/A (Indefinido/Otros)

---

### 5. Ecosistemas e Integración

- **Angular (Front-End):**  
  Proporciona una interfaz de usuario moderna que se comunica mediante APIs RESTful.
  
- **Flask (Python):**  
  Hospeda el modelo de IA y realiza cálculos matemáticos complejos.
  
- **ASP.NET:**  
  Implementa servicios adicionales y lógica de negocio a través de endpoints estandarizados.
  
- **Integración:**  
  La coordinación de Angular, Flask y ASP.NET garantiza una operación sincrónica y escalable.

---

### 6. Arquitectura y Componentes

- **DataWise.AI:**  
  Contiene el módulo de IA desarrollado en Python con un modelo TextCNN, accesible a través de endpoints Flask.
  
- **DataWise.Api:**  
  Capa API basada en ASP.NET que conecta el módulo de IA con la interfaz de usuario.
  
- **DataWise.Client:**  
  Aplicación front-end desarrollada en Angular.
  
- **DataWise.Core:**  
  Servicios y lógica de negocio implementados en .NET.
  
- **DataWise.Data:**  
  Gestión y almacenamiento de datos en bases de datos relacionales y no relacionales.
  
- **DataWise.Common:**  
  Constantes y funciones auxiliares reutilizables.

---

### 7. Funcionalidades

**Funcionalidades Actuales:**
- **Ejecución Local del Modelo:**  
  El modelo procesa el texto de entrada y realiza una clasificación automática.
  
- **Interfaz Web con Categorización:**  
  Los usuarios ingresan texto y reciben retroalimentación inmediata.
  
- **Knowledge Nexus:**  
  Módulo educativo con código fuente, descripciones, comparaciones y otros detalles para preparación de entrevistas.
  
- **Data Chartizer:**  
  Permite la carga y procesamiento rápido de grandes datasets para generar gráficos personalizados.

---

### 8. Implementación – Arquitectura del Módulo de IA (TextCNN)

1. **Parámetros de Entrada e Inicialización:**
   - **vocab_size:** Número de palabras únicas.  
   - **embedding_dim:** Dimensión del vector de cada palabra.  
   - **max_len:** Número máximo de palabras por oración.  
   - **num_filters:** Número de filtros para cada tamaño.  
   - **filter_sizes:** Lista (ej. 2, 3, 4) que define el tamaño del filtro.  
   - **num_classes:** Número de categorías.  
   - **learning_rate:** Tasa de aprendizaje.

2. **Capa de Embedding:**
   - Transforma índices de palabras en vectores continuos.  
   - **Dimensiones:**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Transformación:  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \Rightarrow X_{embedded} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$

3. **Capas Convolucionales:**
   - Para cada filtro de tamaño \( f \):  
     $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
   - Convolución:  
     $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$

4. **Max Pooling y Concatenación:**
   - **Max Pooling:**  
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Concatenación:**  
     $$ p = [p(f_1);\, p(f_2);\, \dots;\, p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$

5. **Capa Completamente Conectada:**
   - Transformación a logits:  
     $$ \text{logits} = p \cdot W_{fc} + b_{fc}, \quad W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}} $$
   - **Softmax:**  
     $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$

6. **Backpropagation:**
   - Cálculo de gradientes:  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - Actualización de parámetros:  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$

---

### 9. Conclusión

DataWise representa una solución integral e innovadora para preparar a los candidatos en entrevistas técnicas. La combinación de algoritmos matemáticos avanzados y una arquitectura modular proporciona tanto conocimientos teóricos como habilidades prácticas esenciales para el éxito.

---

### 10. Licencia y Contacto

**Licencia:**  
Este proyecto está licenciado bajo la [MIT License](LICENSE).

**Contacto:**  
- **Alex Ivailov Stefanov**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)

---

## Deutsch

### 1. Autoren und Betreuer

- **Alex Ivailov Stefanov**  
  - Adresse: Kazanlak, „Dobri Kehayov“ Str. Nr. 13  
  - Telefon: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - Schule: PPMG „Nikola Obreschkov“  
  - Klasse: 11B  

- **Betreuer: Zdravka Stefanova Dimitrova**  
  - Telefon: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Position: Lehrerin für Informatik und Informationstechnologien

---

### 2. Projektübersicht und Ziele

**Ziele:**  
DataWise richtet sich an angehende Programmierer, indem es eine Plattform zur Vertiefung der Kenntnisse in Datenstrukturen und Algorithmen bereitstellt – essenziell für technische Interviews bei führenden Technologieunternehmen (z. B. Facebook, Amazon, Netflix, Google).

**Kontext:**  
Technische Interviews erfordern ein tiefes Verständnis von Algorithmen und Datenstrukturen. DataWise kombiniert ein eigens entwickeltes KI-Modell (komplett in Python ohne externe Bibliotheken) mit traditionellen Lernressourcen.

**Projektphasen:**
- Ideenfindung  
- Architekturgestaltung  
- Konfiguration des Ökosystems  
- Studium der künstlichen Intelligenz  
- Aufbau der KI-Modellarchitektur  
- Lernen von Front-End (TypeScript, Angular) und Python  
- Erstellung eines Datensatzes (~10.000 Beispiele)  
- Implementierung des KI-Modells  
- Logo- und Designentwicklung  
- Testen, Optimierung und Feedback

---

### 3. Mathematische Herausforderungen

- **Lineare Algebra und Matrixoperationen:**  
  Wichtige Operationen wie Matrizenmultiplikation, Transposition und Inversion sind entscheidend für das Training.
  
- **Aktivierungsfunktion und Differenzierbarkeit:**  
  Es wird ReLU verwendet, die differenzierbar sein muss, um Backpropagation korrekt durchzuführen.
  
- **Optimierung und Gradientenabstieg:**  
  Einsatz von stochastischem Gradientenabstieg (SGD) mit adaptiver Lernrate und Momentum.
  
- **Backpropagation:**  
  Komplexe Kettenregel-Berechnungen zur Weitergabe von Gradienten.
  
- **Normalisierung und Regularisierung:**  
  Batch Normalization und L2-Regularisierung werden zur Stabilisierung des Trainings eingesetzt.

---

### 4. Datensatzdetails

- **Quelle und Urheberrecht:**  
  Alle Daten wurden persönlich gesammelt und vorbereitet und sind unter der MIT-Lizenz geschützt.
  
- **Größe und Struktur:**  
  Ein Datensatz mit nahezu 10.000 Beispielen zur Ausbildung und Validierung.
  
- **Kategorien:**  
  Die Daten werden in 10 Kategorien unterteilt:
  - BFS (Breitensuche)
  - DFS (Tiefensuche)
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Nicht definiert/Andere)

---

### 5. Ökosysteme und Integration

- **Angular (Front-End):**  
  Bietet eine moderne, interaktive Benutzeroberfläche, die über RESTful APIs kommuniziert.
  
- **Flask (Python):**  
  Hoster des KI-Modells und Durchführung komplexer mathematischer Berechnungen.
  
- **ASP.NET:**  
  Implementiert zusätzliche Dienste und Geschäftslogik über standardisierte Endpoints.
  
- **Integration:**  
  Die synchronisierte Zusammenarbeit von Angular, Flask und ASP.NET ermöglicht eine skalierbare Lösung.

---

### 6. Architektur und Komponenten

- **DataWise.AI:**  
  Enthält das in Python entwickelte KI-Modul mit einem TextCNN-Modell, zugänglich über Flask.
  
- **DataWise.Api:**  
  Eine auf ASP.NET basierende API-Schicht, die das KI-Modul mit der Benutzeroberfläche verbindet.
  
- **DataWise.Client:**  
  Das Front-End, entwickelt mit Angular.
  
- **DataWise.Core:**  
  Zentrale Dienste und Geschäftslogik, implementiert in .NET.
  
- **DataWise.Data:**  
  Verwaltung und Speicherung von Daten in relationalen und nicht-relationalen Datenbanken.
  
- **DataWise.Common:**  
  Gemeinsame Konstanten und wiederverwendbare Hilfsfunktionen.

---

### 7. Funktionalitäten

**Aktuelle Funktionen:**
- **Lokale Ausführung des Modells:**  
  Das Modell verarbeitet Eingabedaten und führt eine automatische Klassifikation durch.
  
- **Webbasierte Kategorisierung:**  
  Benutzer geben Text ein und erhalten sofortiges Feedback.
  
- **Knowledge Nexus:**  
  Ein Bildungsmodul mit Quellcode, Beschreibungen, Vergleichen und weiteren Informationen zur Vorbereitung auf Interviews oder Prüfungen.
  
- **Data Chartizer:**  
  Ermöglicht die schnelle Verarbeitung großer Datensätze zur Erstellung personalisierter Diagramme.

---

### 8. Implementierungsdetails – TextCNN Architektur

1. **Eingabeparameter und Initialisierung:**
   - **vocab_size:** Anzahl der einzigartigen Wörter.  
   - **embedding_dim:** Dimension des Wortvektors.  
   - **max_len:** Maximale Anzahl von Wörtern pro Satz.  
   - **num_filters:** Anzahl der Filter pro Größe.  
   - **filter_sizes:** Liste (z. B. 2, 3, 4) für die Filtergröße.  
   - **num_classes:** Anzahl der Klassen.  
   - **learning_rate:** Lernrate.

2. **Embedding-Schicht:**
   - Wandelt Wortindizes in kontinuierliche Vektoren um.  
   - **Dimensionen:**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Transformation:  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \Rightarrow X_{embedded} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$

3. **Convolutional Layers:**
   - Für jede Filtergröße \( f \):  
     $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
   - Operation:  
     $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$

4. **Max Pooling und Konkatenation:**
   - **Max Pooling:**  
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Konkatenation:**  
     $$ p = [p(f_1);\, p(f_2);\, \dots;\, p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$

5. **Fully Connected Layer:**
   - Berechnung der Logits:  
     $$ \text{logits} = p \cdot W_{fc} + b_{fc}, \quad W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}} $$
   - **Softmax:**  
     $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$

6. **Backpropagation:**
   - Gradientenberechnung:  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - Parameterupdate:  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$

---

### 9. Fazit

DataWise demonstriert ein hohes technisches Potenzial und bietet eine umfassende Lösung zur Vorbereitung auf technische Interviews. Die Kombination aus fortschrittlichen mathematischen Algorithmen und einer modularen Architektur vermittelt sowohl theoretisches Wissen als auch praktische Fähigkeiten.

---

### 10. Lizenz und Kontakt

**Lizenz:**  
Dieses Projekt steht unter der [MIT License](LICENSE).

**Kontakt:**  
- **Alex Ivailov Stefanov**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Betreuerin: Zdravka Stefanova Dimitrova**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)

---

## Português

### 1. Autores e Supervisor

- **Alex Ivailov Stefanov**  
  - Endereço: Kazanlak, “Dobri Kehayov” St. Nº 13  
  - Telefone: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - Escola: PPMG “Nikola Obreschkov”  
  - Turma: 11B  

- **Supervisor: Zdravka Stefanova Dimitrova**  
  - Telefone: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Cargo: Professora de Informática e Tecnologias da Informação

---

### 2. Resumo e Objetivos

**Objetivos:**  
DataWise foi criado para preparar futuros programadores, fornecendo uma plataforma para aprimorar conhecimentos em estruturas de dados e algoritmos – habilidades essenciais para entrevistas em grandes empresas de tecnologia (e.g., Facebook, Amazon, Netflix, Google).

**Contexto:**  
Entrevistas técnicas demandam um profundo entendimento de algoritmos e estruturas de dados. DataWise combina um modelo de IA personalizado (desenvolvido inteiramente em Python sem bibliotecas externas) com recursos educacionais tradicionais.

**Fases do Projeto:**
- Formulação da ideia  
- Design arquitetônico  
- Configuração do ecossistema  
- Estudo de Inteligência Artificial  
- Construção da arquitetura do modelo de IA  
- Aprendizado de Front-End (TypeScript, Angular) e Python  
- Criação de um dataset (~10.000 exemplos)  
- Implementação do modelo de IA  
- Desenvolvimento de logotipo e design  
- Testes, otimização e coleta de feedback

---

### 3. Desafios Matemáticos

- **Álgebra Linear e Operações Matriciais:**  
  Operações como multiplicação, transposição e inversão de matrizes são fundamentais para o treinamento.
  
- **Função de Ativação e Diferenciabilidade:**  
  A função ReLU é utilizada e deve ser diferenciável para o backpropagation.
  
- **Otimização e Gradiente Descendente:**  
  Uso de SGD com técnicas adaptativas e momentum para evitar mínimos locais.
  
- **Backpropagation:**  
  Cálculo e propagação de gradientes através de operações não lineares.
  
- **Normalização e Regularização:**  
  Batch normalization e L2 regularização são aplicadas para estabilidade do treinamento.

---

### 4. Detalhes do Dataset

- **Fonte e Direitos:**  
  Todos os dados foram coletados e preparados pessoalmente e estão protegidos pela licença MIT.
  
- **Tamanho e Estrutura:**  
  Um dataset com cerca de 10.000 exemplos para treinamento e validação.
  
- **Categorias:**  
  Os dados são divididos em 10 categorias:
  - BFS (Busca em Largura)
  - DFS (Busca em Profundidade)
  - Two Pointers
  - Dynamic Programming
  - Greedy Algorithm
  - Backtracking
  - Binary Search
  - Disjoint Set
  - Game Theory
  - N/A (Indefinido/Outros)

---

### 5. Ecossistemas e Integração

- **Angular (Front-End):**  
  Oferece uma interface moderna que se comunica via APIs RESTful.
  
- **Flask (Python):**  
  Hospeda o modelo de IA e realiza cálculos matemáticos complexos.
  
- **ASP.NET:**  
  Implementa serviços adicionais e lógica de negócio através de endpoints padronizados.
  
- **Integração:**  
  A coordenação entre Angular, Flask e ASP.NET garante uma solução escalável e integrada.

---

### 6. Arquitetura e Componentes

- **DataWise.AI:**  
  Contém o módulo de IA desenvolvido em Python com um modelo TextCNN, acessível via endpoints Flask.
  
- **DataWise.Api:**  
  Camada de API baseada em ASP.NET que conecta o módulo de IA à interface do usuário.
  
- **DataWise.Client:**  
  Aplicação front-end desenvolvida com Angular.
  
- **DataWise.Core:**  
  Serviços centrais e lógica de negócio implementados em .NET.
  
- **DataWise.Data:**  
  Gerenciamento e armazenamento de dados em bancos de dados relacionais e não relacionais.
  
- **DataWise.Common:**  
  Constantes e funções auxiliares reutilizáveis.

---

### 7. Funcionalidades

**Funcionalidades Atuais:**
- **Execução Local do Modelo:**  
  O modelo processa os dados de entrada e realiza classificação automática.
  
- **Interface Web com Categorização:**  
  Usuários inserem texto e recebem feedback imediato.
  
- **Knowledge Nexus:**  
  Módulo educativo com código-fonte, descrições, comparações e informações adicionais para preparação.
  
- **Data Chartizer:**  
  Permite o processamento rápido de grandes datasets para gerar gráficos personalizados.

---

### 8. Detalhes de Implementação – Arquitetura do Módulo de IA (TextCNN)

1. **Parâmetros de Entrada e Inicialização:**
   - **vocab_size:** Número de palavras únicas.  
   - **embedding_dim:** Dimensão do vetor de cada palavra.  
   - **max_len:** Número máximo de palavras por sentença.  
   - **num_filters:** Número de filtros para cada tamanho.  
   - **filter_sizes:** Lista (ex.: 2, 3, 4) definindo o tamanho do filtro.  
   - **num_classes:** Número de categorias.  
   - **learning_rate:** Taxa de aprendizagem.

2. **Camada de Embedding:**
   - Converte índices de palavras em vetores contínuos.  
   - **Dimensões:**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Transformação:  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \Rightarrow X_{embedded} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$

3. **Camadas Convolucionais:**
   - Para cada tamanho de filtro \( f \):  
     $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
   - Operação de convolução:  
     $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$

4. **Max Pooling e Concatenção:**
   - **Max Pooling:**  
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Concatenção:**  
     $$ p = [p(f_1);\, p(f_2);\, \dots;\, p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$

5. **Camada Totalmente Conectada:**
   - Cálculo dos logits:  
     $$ \text{logits} = p \cdot W_{fc} + b_{fc}, \quad W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}} $$
   - **Softmax:**  
     $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$

6. **Backpropagation:**
   - Cálculo do gradiente:  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - Atualização dos parâmetros:  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$

---

### 9. Conclusão

DataWise representa uma solução abrangente e inovadora para preparar candidatos para entrevistas técnicas. A combinação de algoritmos matemáticos avançados e uma arquitetura modular oferece tanto conhecimento teórico quanto habilidades práticas essenciais.

---

### 10. Licença e Contato

**Licença:**  
Este projeto está licenciado sob a [MIT License](LICENSE).

**Contato:**  
- **Alex Ivailov Stefanov**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Supervisor: Zdravka Stefanova Dimitrova**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)

---

## Français

### 1. Auteurs et Superviseur

- **Alex Ivailov Stefanov**  
  - Adresse: Kazanlak, rue “Dobri Kehayov” No. 13  
  - Téléphone: 0889475177  
  - Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
  - École: PPMG “Nikola Obreschkov”  
  - Classe: 11B  

- **Superviseur: Zdravka Stefanova Dimitrova**  
  - Téléphone: 0893422519  
  - Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)  
  - Poste: Enseignante en Informatique et Technologies de l'Information

---

### 2. Résumé et Objectifs

**Objectifs :**  
DataWise vise à préparer les futurs programmeurs en fournissant une plateforme d'apprentissage des structures de données et des algorithmes – compétences essentielles pour les entretiens techniques dans les grandes entreprises technologiques (e.g., Facebook, Amazon, Netflix, Google).

**Contexte :**  
Les entretiens techniques exigent une compréhension approfondie des algorithmes et des structures de données. DataWise combine un modèle d'IA personnalisé (développé intégralement en Python sans bibliothèques externes) avec des ressources éducatives traditionnelles.

**Phases du Projet :**
- Formulation de l'idée  
- Conception architecturale  
- Configuration de l'écosystème  
- Étude de l'IA  
- Construction de l'architecture du modèle  
- Apprentissage du Front-End (TypeScript, Angular) et de Python  
- Création d'un dataset (~10 000 exemples)  
- Implémentation du modèle d'IA  
- Développement du logo et du design  
- Tests, optimisation et collecte de feedback

---

### 3. Défis Mathématiques

- **Algèbre Linéaire et Opérations Matricielles :**  
  Les opérations comme la multiplication, la transposition et l'inversion des matrices sont essentielles pour l'entraînement du modèle.
  
- **Fonction d'Activation et Différentiabilité :**  
  La fonction ReLU est utilisée et doit être différentiable pour une rétropropagation efficace.
  
- **Optimisation et Descente de Gradient :**  
  Utilisation de la descente de gradient stochastique (SGD) avec des techniques adaptatives et du momentum.
  
- **Backpropagation :**  
  Calcul et propagation des gradients à travers des opérations non linéaires.
  
- **Normalisation et Régularisation :**  
  L'application de batch normalization et de régularisation L2 assure la stabilité de l'entraînement.

---

### 4. Détails du Dataset

- **Source et Droits :**  
  Toutes les données ont été collectées et préparées personnellement et sont protégées par la licence MIT.
  
- **Taille et Structure :**  
  Un dataset de près de 10 000 exemples pour l'entraînement et la validation.
  
- **Catégories :**  
  Les données sont divisées en 10 catégories :
  - BFS (Recherche en largeur)
  - DFS (Recherche en profondeur)
  - Two Pointers  
  - Dynamic Programming  
  - Greedy Algorithm  
  - Backtracking  
  - Binary Search  
  - Disjoint Set  
  - Game Theory  
  - N/A (Indéfini/Autres)

---

### 5. Écosystèmes et Intégration

- **Angular (Front-End) :**  
  Fournit une interface utilisateur moderne qui communique via des APIs RESTful.
  
- **Flask (Python) :**  
  Héberge le modèle d'IA et effectue des calculs mathématiques complexes.
  
- **ASP.NET :**  
  Implémente des services additionnels et la logique métier via des endpoints standardisés.
  
- **Intégration :**  
  La coordination entre Angular, Flask et ASP.NET garantit une solution synchronisée et évolutive.

---

### 6. Architecture et Composants

- **DataWise.AI :**  
  Contient le module d'IA développé en Python, avec un modèle TextCNN accessible via des endpoints Flask.
  
- **DataWise.Api :**  
  Une couche API basée sur ASP.NET qui relie le module d'IA à l'interface utilisateur.
  
- **DataWise.Client :**  
  L'application front-end développée en Angular.
  
- **DataWise.Core :**  
  Fournit les services centraux et la logique métier en .NET.
  
- **DataWise.Data :**  
  Gère le stockage et l'accès aux données (bases de données relationnelles et non relationnelles).
  
- **DataWise.Common :**  
  Constantes communes et fonctions utilitaires réutilisables.

---

### 7. Fonctionnalités

**Fonctionnalités Actuelles :**
- **Exécution Locale du Modèle :**  
  Le modèle traite les données d'entrée et effectue une classification automatique.
  
- **Interface Web avec Catégorisation :**  
  Les utilisateurs saisissent du texte et reçoivent un retour immédiat.
  
- **Knowledge Nexus :**  
  Un module éducatif fournissant du code source, des descriptions, des comparaisons et d'autres informations pour la préparation aux entretiens ou examens.
  
- **Data Chartizer :**  
  Permet le traitement rapide de grands datasets pour générer des graphiques personnalisés.

---

### 8. Détails de l'Implémentation – Architecture du Modèle TextCNN

1. **Paramètres d'Entrée et Initialisation :**
   - **vocab_size :** Nombre de mots uniques.  
   - **embedding_dim :** Dimension de chaque vecteur de mot.  
   - **max_len :** Nombre maximal de mots par phrase.  
   - **num_filters :** Nombre de filtres par taille.  
   - **filter_sizes :** Liste (ex. 2, 3, 4) définissant la taille du filtre.  
   - **num_classes :** Nombre de catégories.  
   - **learning_rate :** Taux d'apprentissage.

2. **Couche d'Embedding :**
   - Convertit les indices de mots en vecteurs continus.  
   - **Dimensions :**  
     $$ E \in \mathbb{R}^{\text{vocab\_size} \times \text{embedding\_dim}} $$
   - Transformation :  
     $$ X \in \mathbb{R}^{N \times \text{max\_len}} \Rightarrow X_{embedded} \in \mathbb{R}^{N \times \text{max\_len} \times \text{embedding\_dim}} $$

3. **Couches Convolutionnelles :**
   - Pour chaque taille de filtre \( f \) :  
     $$ W(f) \in \mathbb{R}^{f \times \text{embedding\_dim} \times \text{num\_filters}} $$
   - Opération de convolution :  
     $$ Z_{i,k}(f) = \text{ReLU}\left(\sum_{j=1}^{f} \sum_{d=1}^{\text{embedding\_dim}} X_{i+j-1,d} \cdot W_{j,d,k}(f)\right) $$

4. **Max Pooling et Concatenation :**
   - **Max Pooling :**  
     $$ p_k(f) = \max_{1 \le i \le \text{max\_len}-f+1} Z_{i,k}(f) $$
   - **Concatenation :**  
     $$ p = [p(f_1);\, p(f_2);\, \dots;\, p(f_m)] \in \mathbb{R}^{N \times (\text{num\_filters} \times m)} $$

5. **Couche Complètement Connectée :**
   - Calcul des logits :  
     $$ \text{logits} = p \cdot W_{fc} + b_{fc}, \quad W_{fc} \in \mathbb{R}^{\text{fc\_input\_dim} \times \text{num\_classes}} $$
   - **Softmax :**  
     $$ \hat{y}_{i,c} = \frac{e^{\text{logits}_{i,c}}}{\sum_{c'=1}^{\text{num\_classes}} e^{\text{logits}_{i,c'}}} $$

6. **Backpropagation :**
   - Calcul du gradient :  
     $$ \frac{\partial \text{Loss}}{\partial \text{logits}} = \frac{\hat{y} - y}{N} $$
   - Mise à jour des paramètres :  
     $$ \theta \leftarrow \theta - \eta \cdot \frac{\partial \text{Loss}}{\partial \theta} $$

---

### 9. Conclusion

DataWise démontre un potentiel technique élevé et offre une solution complète pour préparer les candidats aux entretiens techniques. La combinaison d'algorithmes mathématiques avancés et d'une architecture modulaire permet d'acquérir à la fois des connaissances théoriques et des compétences pratiques indispensables.

---

### 10. Licence et Contact

**Licence :**  
Ce projet est sous licence [MIT License](LICENSE).

**Contact :**  
- **Alex Ivailov Stefanov**  
  Email: [rlgalexbgto@gmail.com](mailto:rlgalexbgto@gmail.com)  
- **Superviseure : Zdravka Stefanova Dimitrova**  
  Email: [dimitrova@pmgkk.com](mailto:dimitrova@pmgkk.com)
