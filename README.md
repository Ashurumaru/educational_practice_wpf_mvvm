<body>
    <h1>Educational Practice Project</h1>
    <h2>Описание</h2>
    <p>Данный проект был разработан в рамках учебной практики. Этот проект представляет собой систему управления пользователями, разработанную с использованием технологии Windows Presentation Foundation (WPF) и языка программирования C#.</p>
    <h2>Функциональность</h2>
    <h3>Основные функции:</h3>
    <ul>
        <li><b>Регистрация и вход в систему:</b> Пользователи могут войти или же зарегистрировать свои учетные записи, предоставив необходимую информацию. Зарегистрированные пользователи могут войти в систему, используя свой логин и пароль.</li>
        <li><strong>Личный кабинет:</strong> Пользователи могут видеть свой профиль и взаимодействовать с базой данных, если имеют соответствующий уровень доступа. (если они вошли в систему).</li>
        <li><strong>Уровень доступа:</strong> Администраторы имеют доступ к функциям управления пользователями. Администраторы могут добавлять, обновлять и удалять учетные записи пользователей в базе данных. Обычные пользователи ограничиваются просмотром базы данных пользователей.</li>
        <li><strong>Выбор темы оформления:</strong> Пользователи могут выбирать темы оформления для интерфейса приложения.</li>
        <li><strong>Смена фона приложения:</strong> Реализована функциональность загрузки и отображения изображений на фоне.</li>
    </ul> 
  <h2>Структура проекта</h2>
  <h3>Папка Models</h3>
   <p>В этой папке содержатся классы, отвечающие за взаимодействие с базой данных и логикой приложения:</p>
   <ul>
        <li><strong>DataBaseLogicModel.cs:</strong> Класс, реализующий взаимодействие с базой данных для проверки логина, добавления, обновления и удаления пользователей, а также получения информации о пользователях.</li>
        <li><strong>ThemeManager.cs:</strong> Менеджер тем, обеспечивающий загрузку и управление изображениями.</li>
        <li><strong>UserModel.cs:</strong> Модель пользователя с полями, такими как Id, Login, Password, FirstName, LastName, MiddleName и AccessLevel.</li>
    </ul> 
      <h3>Папка ViewModels</h3>
       <p>В этой папке размещены классы, управляющие логикой представлений:</p>
   <ul>
        <li><strong>BaseViewModel.cs:</strong>Базовый класс для всех ViewModel, реализующий интерфейс INotifyPropertyChanged для обновления представлений.</li>
        <li><strong>RelayCommand.cs:</strong> Реализация команды, используемой для связывания действий в представлениях с методами в ViewModel.</li>
        <li><strong>PersonalAccountViewModel.cs:</strong> ViewModel, управляющая логикой личного кабинета, включая отображение, добавление, обновление, удаление пользователей, а также сменой тем и фона.</li>
        <li><strong>LoginViewModel.cs: </strong>ViewModel для логики входа в систему, регистрации и обработки ошибок в процессе входа.</li>
        <li><strong>MessageBoxViewModel.cs:</strong> ViewModel для управления окном MessageBox</li>
    </ul> 
    <h3>Папка View</h3>
       <p>В этой папке содержатся классы для отображения пользовательского интерфейса:</p>
   <ul>
        <li><strong>LoginView.cs:</strong> Представление для входа в систему и регистрации.</li>
        <li><strong>PersonalAccountView.cs:</strong> Представление для личного кабинета пользователя.</li>
        <li><strong>MessageBoxView.cs:</strong> Представление для отображения MessageBox.</li>
    </ul> 
    <li><strong>DataBaseConfig.cs:</strong> Содержит настройки для подключения к базе данных.</li>
      <h2>Технологии и Инструменты</h2>
    <ul>
        <li><strong>WPF и C#:</strong> Проект создан с использованием технологии WPF и языка программирования C#.</li>
        <li><strong>Entity Framework:</strong> Используется для взаимодействия с базой данных SQL Server.</li>
        <li><strong>MVVM:</strong> Проект следует паттерну MVVM (Model-View-ViewModel) для более эффективного управления данными и представлением.</li>
    </ul>
        <h2>Примеры</h2>
        <h3>Окно входа</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/e6062cf8-37d5-4059-b904-42f644beaf21'></video>
        <h3>Окно регистрации</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/7c96c64e-4971-4410-8532-582ff9afa730'></video>
        <h3>Интерфейс обычного пользователя</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/fcdf61dc-24e0-4ff7-9be0-18fd0179a2dd' width=180></video>
        <h3>Интерфейс Администратора</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/a802e497-a0ff-47b1-8ace-a6c90377d04a'></video>
        <h3>Возможности администратора</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/d5e53f81-6615-4116-abd3-7b924de1ced4' width=180></video>
        <h3>Стиль Dark</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/595fea06-db9c-42f4-afa2-55a5c1622743' width=180></video>
        <h3>Стиль White</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/340f4a3b-9245-41d5-ad68-2f4dfffdca6d' width=180></video>
        <h3>Смена фона</h3>
        <video src='https://github.com/Ashurumaru/educational_practice_wpf_mvvm/assets/86610969/18122cc0-b646-4783-8392-ece77157e1b0' width=180></video>
    <h2>Контакты</h2>
    <li>Автор: <a href="https://github.com/Ashurumaru/">ashuramaru</a></li>
</body>
