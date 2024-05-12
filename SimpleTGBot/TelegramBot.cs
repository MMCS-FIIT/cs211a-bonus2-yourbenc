using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleTGBot;

using System.Data.SqlTypes;
using System.Formats.Asn1;
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
    private static string CoolNicknames = $"KAHUCTPA_CTAPOrO_/7UBA\n-=JIEB0JlU6EPAJI=-\nCtPoūMaTePuAJIbl\n[CMyTa]/7paBocλaBHblú_/7apeHb\n^ocTpbIe.^.ko3bIpbku^[adidas///]\nHET.nOCTOU.9.UCnPABλIOCb\nync...Tbl-MEPTB\n[JAM]ku6ep_rAHg]|[y6ac\n.:.:.:. KpoJIuK .:.:.:.\n-λAga_rPaHTa - (HoMep-_-CKpblT)-\nCoM_Kλ|-0HyJI_Ha_4epB9l[WtF?(⊙_⊙)?]\n7oMyHKyJI-_-Vs-_-CJIoBapb\n[PRO]ka3Huk_keMepoBo\ncblH_dust2x2\nx✞x.AHrEJI.XOPOHuTEJIb.x✞x\n[6e36aLLIEHHb|ŭ] KPAH\n---NISSAN---\nOKyHb_B_CyI7E\nhihihi_smotrite_4MO\n-jezus777-vs-666bafomet-\nPa6_6yprepa [USA]\n(eXxXTRA)BaraHTHblu*";


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
                if (message.Text.ToLower() == "/start" || message.Text.ToLower() == "/help")
                {
                    Console.WriteLine($"Был нажат /start в чате {chatId}");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"На сервере запрещено:\n*использование читов, распрыжек, скриптов и прочих хитростей;\n*использование спреев - обманок и спреев с эротическим содержанием;\nиспользование уязвимостей карт;\n*мат в любом проявлении\n(слово сука хоть и литературное, но на сервере расценивается как мат);\n*оскорбления игроков;\n*флуд и крики в голосовой чат\nВсё это карается баном\n\nДанный бот переводит ваши сообщения с русского языка на язык серверов CS 1.6. \n\nСписок дополнительных команд:\n/ChangeSeparator - добавить разделитель, ставящийся между словами(крутым парням рекомендуется точка или тире) - по умолчанию пробел\n/ChangeFrames - выбрать рамки для сообщений - по умолчанию отсутствуют\n/GetCoolNicknames - получить коллекцию крутых готовых ников, вручную собранных с серверов 1.6\n/GetCoolKeyboard - получить архив с раскладкой для Windows, которая вместо русских букв пишет BOT_Takue(делал не я!)\n/help или /start - получить этот текст заново",
                        cancellationToken: cancellationToken);
                }
                else if (message.Text.ToLower() == "/changeseparator")
                {
                    Console.WriteLine($"Запрос на смену разделителей в чате {chatId}");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Введите новый разделитель(если хотите вернуть пробел, то напишите 'пробел')",
                        cancellationToken: cancellationToken);
                    state = States.SeparatorChange;
                    return;
                }
                else if (message.Text.ToLower() == "/changeframes")
                {
                    Console.WriteLine($"Запрос на смену рамок в чате {chatId}");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите рамку из предложенного списка:",
                    replyMarkup: framesMarkUp,
                    cancellationToken: cancellationToken);
                    state = States.FrameChange;
                    return;
                }
                else if (message.Text.ToLower() == "/getcoolkeyboard")
                {
                    // Попытался отправить файл, как сказано в документации, но у меня что-то не так, и я так и не понял почему. Из-за этого же не удалось реализовать отправку стикеров, изображений и т.п.
                    // Message newMessage = await botClient.SendDocumentAsync(
                    // chatId: chatId,
                    // document: InputFile.FromUri("https://vk.com/doc652669605_676120367?hash=w6dIn8q8zXzuEIKplHHIZv26JZaJBR9VASJnIlkYgZ4&dl=FKUEuAASuldgtrnlh46pY9NqGwG5Rz1vrTRIV55TBFo"),
                    // parseMode: ParseMode.Html,
                    // cancellationToken: cancellationToken);
                    // return;
                    Console.WriteLine($"Запрос на скачивание клавиатуры в чате {chatId}");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "А ФИГУШКИ потому что у меня ошибка с отправлением файлов, которую я не понял, как решить. Только такая ссылка: https://vk.cc/cwKzIs (раскладка заменяет якутскую!)",
                        cancellationToken: cancellationToken);
                }
                else if (message.Text.ToLower() == "/getcoolnicknames")
                {
                    Console.WriteLine($"Запрос крутых ников в чате {chatId}");
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: CoolNicknames,
                        cancellationToken: cancellationToken);
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
                Console.WriteLine($"Получено сообщение в чате {chatId}: геолокация");
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
            else
            {
                Console.WriteLine($"Получено сообщение в чате {chatId}: что-то левое");
                Message newMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Напоминаю: флуд карается БАНОМ",
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
                text: "Что-то так себе разделитель. Скиньте, пожалуйста, нормальный",
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
                right_frame = "_xXx";
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