using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models {
    public static class ListExtension {

        public static IList<T> Difference<T>(this IList<T> minuend, IList<T> subtrahend) where T : DbEntities {

            minuend.ToList().ForEach(x => {
                if (subtrahend.ToList().Exists(z => z == x))
                    minuend.Remove(x);
            });

            return minuend;
        }
    }
}
