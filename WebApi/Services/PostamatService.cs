using System.Collections.Generic;
using System.Linq;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Services
{
    public class PostamatService
    {
        private readonly List<Postamat> _postamats;

        public PostamatService(List<Postamat> postamats)
        {
            _postamats = postamats;
        }

        public Postamat GetPostamat(string postamatId)
        {
            var postamat = _postamats.FirstOrDefault(x => x.Id == postamatId);
            if (postamat == null)
                throw new NotFoundException($"Postamat with Id '{postamatId} not found'");
            return postamat;
        }
    }
}