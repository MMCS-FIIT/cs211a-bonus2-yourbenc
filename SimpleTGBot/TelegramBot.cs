using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleTGBot;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TelegramBot
{
    private const string BotToken = "7058950530:AAEGodQkvldWalK2ukcQU2CxDvqUiPhWFVY";
    public string separator = " ";
    public string left_frame = "";
    public string right_frame = "";
    private States state = States.Normal;


    /// <summary>
    /// Инициализирует и обеспечивает работу бота до нажатия клавиши Esc
    /// </summary>
    public async Task Run()
    {
        // Если вам нужно хранить какие-то данные во время работы бота (массив информации, логи бота,
        // историю сообщений для каждого пользователя), то это всё надо инициализировать в этом методе.

        // Инициализируем наш клиент, передавая ему токен.
        var botClient = new TelegramBotClient(BotToken);

        // Служебные вещи для организации правильной работы с потоками
        using CancellationTokenSource cts = new CancellationTokenSource();

        // Разрешённые события, которые будет получать и обрабатывать наш бот.
        // Будем получать только сообщения. При желании можно поработать с другими событиями.
        ReceiverOptions receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new[] { UpdateType.Message }
        };

        // Привязываем все обработчики и начинаем принимать сообщения для бота
        botClient.StartReceiving(
            updateHandler: OnMessageReceived,
            pollingErrorHandler: OnErrorOccured,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        // Проверяем что токен верный и получаем информацию о боте
        var me = await botClient.GetMeAsync(cancellationToken: cts.Token);
        Console.WriteLine($"Бот @{me.Username} запущен.\nДля остановки нажмите клавишу Esc...");

        // Ждём, пока будет нажата клавиша Esc, тогда завершаем работу бота
        while (Console.ReadKey().Key != ConsoleKey.Escape) { }

        // Отправляем запрос для остановки работы клиента.
        cts.Cancel();
    }

    /// <summary>
    /// Обработчик события получения сообщения.
    /// </summary>
    /// <param name="botClient">Клиент, который получил сообщение</param>
    /// <param name="update">Событие, произошедшее в чате. Новое сообщение, голос в опросе, исключение из чата и т. д.</param>
    /// <param name="cancellationToken">Служебный токен для работы с многопоточностью</param>
    async Task OnMessageReceived(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Работаем только с сообщениями. Остальные события игнорируем
        var message = update.Message;
        if (message is null)
        {
            return;
        }
        // мой маркап
        ReplyKeyboardMarkup framesMarkUp = new(new[]
                {
                    new KeyboardButton[] { "xXx_npuMep_xXx", "꧁npuMep꧂" },
                    new KeyboardButton[] { "☜❶☞npuMep☜❶☞","npuMep" },
                })
        {
            ResizeKeyboard = true
        };
        // Получаем ID чата, в которое пришло сообщение.
        var chatId = message.Chat.Id;

        if (state == States.Normal)
        {
            if (message.Text != null)
            {
                if (message.Text.ToLower() == "/start")
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"На сервере запрещено:\n*использование читов, распрыжек, скриптов и прочих хитростей;\n*использование спреев - обманок и спреев с эротическим содержанием;\nиспользование уязвимостей карт;\n*мат в любом проявлении\n(слово сука хоть и литературное, но на сервере расценивается как мат);\n*оскорбления игроков;\n*использование нечитаемых ников, ников содежащих мат, цифровых ников.\nВсё это карается баном\n\nСписок команд:\n/ChangeSeparator - добавить разделитель, ставящийся между словами(крутым парням рекомендуется точка или тире) - по умолчанию пробел\n/ChangeFrames - выбрать рамки для сообщений",
                        cancellationToken: cancellationToken);
                }
                else if (message.Text.ToLower() == "/changeseparator")
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Введите новый разделитель(если хотите вернуть пробел, то напишите 'пробел')",
                        cancellationToken: cancellationToken);
                    state = States.SeparatorChange;
                    return;
                }
                else if (message.Text.ToLower() == "/changeframes")
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите рамку из предложенного списка:",
                    replyMarkup: framesMarkUp,
                    cancellationToken: cancellationToken);
                    state = States.FrameChange;
                    return;
                }
                else
                {
                    Console.WriteLine($"Получено сообщение в чате {chatId}: '{message.Text}'");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: Functions.TranslateMessage(message.Text, separator, left_frame, right_frame),
                        cancellationToken: cancellationToken);
                }
            }
            else if (message.Sticker != null)
            {
                Console.WriteLine($"Получено сообщение в чате {chatId}: стикер");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Классный стикер бро",
                cancellationToken: cancellationToken);
            }
            else if (message.Location != null)
            {
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Адрес записан, ожидайте гостей",
                cancellationToken: cancellationToken);
            }
            else if (message.Video != null)
            {
                Console.WriteLine($"Получено сообщение в чате {chatId}: видео");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Классное видео братан я под столом",
                cancellationToken: cancellationToken);
            }
        }
        // Состояние смены разделителя
        if (state == States.SeparatorChange)
        {
            if (message.Text == null)
            {
                Console.WriteLine($"Попытка изменить разделитель в чате {chatId}: неудача");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Что-то так себе разделитель. Давай по-новой",
                cancellationToken: cancellationToken);
            }
            else
            {
                if (message.Text.ToLower() == "пробел")
                    separator = " ";
                else
                    separator = message.Text;
                Console.WriteLine($"Попытка изменить разделитель в чате {chatId}: успешно");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Разделитель обновлён",
                cancellationToken: cancellationToken);
                state = States.Normal;
            }
        }
        // Состояние смены рамки
        if (state == States.FrameChange)
        {

            if (message.Text == "xXx_npuMep_xXx")
            {
                Console.WriteLine($"Попытка изменить рамку в чате {chatId}: успех");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Рамка обновлена",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
                left_frame = "xXx_";
                left_frame = "_xXx";
                state = States.Normal;
            }
            else if (message.Text == "꧁npuMep꧂")
            {
                Console.WriteLine($"Попытка изменить рамку в чате {chatId}: успех");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Рамка обновлена",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
                left_frame = "꧁";
                right_frame = "꧂";
                state = States.Normal;
            }
            else if (message.Text == "☜❶☞npuMep☜❶☞")
            {
                Console.WriteLine($"Попытка изменить рамку в чате {chatId}: успех");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Рамка обновлена",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
                left_frame = "☜❶☞";
                right_frame = "☜❶☞";
                state = States.Normal;
            }
            else if (message.Text == "npuMep")
            {
                Console.WriteLine($"Попытка изменить рамку в чате {chatId}: успех");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Рамка обновлена",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
                left_frame = "";
                right_frame = "";
                state = States.Normal;
            }
            else
            {
                Console.WriteLine($"Попытка изменить рамку в чате {chatId}: неудача");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "НУ КАКАЯ ЭТО РАМКА????? Выберите, пожалуйста, нормальную",
                replyMarkup: framesMarkUp,
                cancellationToken: cancellationToken);
            }
        }
    }

    /// <summary>
    /// Обработчик исключений, возникших при работе бота
    /// </summary>
    /// <param name="botClient">Клиент, для которого возникло исключение</param>
    /// <param name="exception">Возникшее исключение</param>
    /// <param name="cancellationToken">Служебный токен для работы с многопоточностью</param>
    /// <returns></returns>
    Task OnErrorOccured(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // В зависимости от типа исключения печатаем различные сообщения об ошибке
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",

            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);

        // Завершаем работу
        return Task.CompletedTask;
    }
}