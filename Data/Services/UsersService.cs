using myCredCardsAPI.Data.Models;
using myCredCardsAPI.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCredCardsAPI.Data.Services
{
    public class UsersService
    {
        private AppDbContext _context;
        public UsersService(AppDbContext context)
        {
            _context = context;
        }

        public class CardFinal
        {
            public String CardNumber { get; set; }
        }

        public String GenCard()
        {

            String newcard = "4";
            Random randomnumber = new Random();
            for (int i = 0; i < 15; i++)
            {
                newcard += randomnumber.Next(0, 9).ToString();
            }

            return newcard;
        }


        public Object AddUser(String mail)
        {

            //Guarda o usuário numa variável do tipo User a partir do email.					
            var _user = new User()
            {
                Mail = mail
            };

            // Adiciona o usuário na database, SE não achar nenhum com o mesmo email
            if (_context.Users.Where(u => u.Mail == mail).Count() == 0)
            {
                _context.Users.Add(_user);
            }
            else
            { // Caso ache um com o mesmo email, guarda ele na variável _user. 
                _user = _context.Users.Where(u => u.Mail == mail).FirstOrDefault();
            }


            _context.SaveChanges(); //Salvamos as mudanças na database, caso existam.
            String validCard = GenCard(); //validCard recebe o cartão gerado

            while (_context.Cards.Where(c => c.CardNumber == validCard).Count() != 0)
            {//Enquanto o cartão for repetido, gere outro, até o cartão gerado ser único.
                validCard = GenCard();
            }

            //Creating new valid Card for the user
            var _card = new Card()
            {
                CardNumber = validCard,
                UserId = _user.Id,
                CreatedAt = DateTime.Now
            };

            //Adicionamos o cartão e salvamos as alterações.
            _context.Cards.Add(_card);
            _context.SaveChanges();

            CardFinal CardFinalResponse = new CardFinal { CardNumber = validCard };

            return CardFinalResponse;
        }

        public List<CardVM> GetUserCards(String mail)
        {

            //encontra o usuário pelo email e salva numa variável
            var user = _context.Users.Where(u => u.Mail == mail).FirstOrDefault();

            if (user != null) //checa se ele existe
            {
                //Pega o id do usuário para poder encontrar seus cartões
                var id = user.Id;

                //Cria a lista de cartões seguindo a sequência de queries/limitações
                var _cardnumbers = _context.Cards.Where(n => n.UserId == id)
                    .Select(n => new CardVM() //Cria uma VM para cada cartão encontrado
                    {
                        CardNumber = n.CardNumber,
                        CreatedAt = n.CreatedAt
                    }).OrderBy(t => t.CreatedAt).ToList(); //Lista em rdem cronológica

                return _cardnumbers; //Caso exista, retorna a lista de cartões
            }

            else return null;
        }
    }

}

