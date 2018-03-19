using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace TraducaoBot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        public RootDialog(ILuisService service) : base(service) { }

        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe não consegui te entender.");
            context.Done<string>(null);
        }

        
        
        [LuisIntent("consciencia")]
        public async Task ConscienciaAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eu sou um bot que te ajuda a traduzir tudo para o inglês.");
            context.Done<string>(null);
        }

        
        [LuisIntent("ajudar")]
        public async Task AjudarAsync(IDialogContext context, LuisResult result)
        {
            var response = "Eu ajudo a ** traduzir ** o seu ** texto **";
            await context.PostAsync(response);
            context.Done<string>(null);
        }

       
        [LuisIntent("Cumprimento")]
        public async Task Cumprimento(IDialogContext context, LuisResult result)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).TimeOfDay;
            string saudacao;

            if (now < TimeSpan.FromHours(12)) saudacao = "Bom dia";
            else if (now < TimeSpan.FromHours(18)) saudacao = "Boa tarde";
            else saudacao = "Boa noite";

            await context.PostAsync($"{saudacao}! Em que posso ajudar?");
            context.Done<string>(null);
        }

       
        [LuisIntent("TraducaoBot")]
        public async Task TraducaoBot(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Me diga que eu Traduzo para Inglês.");
            context.Wait(TraducaoEn);
        }

        #region [Métodos internos]

        private async Task TraducaoEn(IDialogContext context, IAwaitable<IMessageActivity> value)
        {
            var message = await value;

            var text = message.Text;

            var response = await new Linguagem().TraducaoEn(text);

            await context.PostAsync(response);
            context.Wait(MessageReceived);
        }
        #endregion
    }

}