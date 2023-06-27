using Azure;
using Azure.AI.OpenAI;
using AzureOpenAISample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzureOpenAISample.Pages
{
    public class IndexModel : PageModel
    {
        const int maximumMessageCount = 10 - 1;

        readonly AzureOpenAIContext _context;

        public IndexModel(AzureOpenAIContext context) => _context = context;

        [BindProperty]
        public IList<Message> Messages { get; set; } = new List<Message>();

        [BindProperty]
        public string UserMessage { get; set; } = "";

        public async Task OnGetAsync()
        {
            if (_context.Messages != null)
                Messages = await _context.Messages.OrderByDescending(message => message.CreatedAt).Take(maximumMessageCount).OrderBy(message => message.CreatedAt).ToListAsync();
        }

        const string clearCommand = "clear";

        ChatMessage systemChatMessage = new (ChatRole.System, @"あなたは私の英会話の相手として、ネイティブ話者として振る舞ってください。
            私の発言に対して、以下のフォーマットで1回に1つずつ回答します。
            説明は書かないでください。まとめて会話内容を書かないでください。

            #フォーマット:
            【修正】:
            {私の英文を自然な英語に直してください。lang:en}
            【理由】:
            {私の英文と、直した英文の差分で、重要なミスがある場合のみ、40文字以内で、日本語で指摘します。lang:ja}
            【返答】:
            {あなたの会話文です。1回に1つの会話のみ出力します。まずは、私の発言に相槌を打ち、そのあと、私への質問を返してください。lang:en}

            #
            私の最初の会話は、""Hello""です。
            毎回、フォーマットを厳格に守り、【修正】、【理由】、【返答】、を必ず出力してください。");

        IList<ChatMessage> ChatMessages => new[] { systemChatMessage }.Concat(Messages.Select(message => new ChatMessage(message.Owner.Equals("User") ? ChatRole.User : ChatRole.Assistant, message.Content))).ToList();

        ChatCompletionsOptions GetChatCompletionsOptions {
            get {
                var chatCompletionsOptions = new ChatCompletionsOptions {
                    Temperature = (float)0.7,
                    MaxTokens = 800,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                };
                foreach (var chatMessage in ChatMessages)
                    chatCompletionsOptions.Messages.Add(chatMessage);

                return chatCompletionsOptions;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserMessage.ToLower().Equals(clearCommand)) {
                _context.Messages.RemoveRange(_context.Messages);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            var message = new Message { Owner = "User", Content = UserMessage };
            Messages.Add(message);

            var client = new OpenAIClient(
                new Uri("https://azureopenai20230511.openai.azure.com/"),
                new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")));

            var chatCompletionsOptions = GetChatCompletionsOptions;

            // ### If streaming is selected
            //Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(deploymentOrModelName: "AzureOpenAISample20230511", chatCompletionsOptions);
            //using StreamingChatCompletions streamingChatCompletions = response.Value;

            // ### If streaming is not selected
            var responseWithoutStream = await client.GetChatCompletionsAsync("AzureOpenAISample20230511", chatCompletionsOptions);

            var completions = responseWithoutStream.Value;
            var responseText = completions.Choices.Count > 0 ? completions.Choices[0].Message.Content : "No response";

            _context.Messages.Add(message);
            _context.Messages.Add(new Message { Owner = "Assistant", Content = responseText });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}