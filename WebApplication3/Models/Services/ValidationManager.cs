using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class ValidationManager {

        public bool CheckMachineryTypeDTO(MachineryTypeDTO dto, out string message) {
            if (!StringIsNullOrEmptry(dto.Name, out message)) return false;
            if (!StringLenght(dto.Name, out message)) return false;
            if (!CheckListCountZero(dto.AreasId, out message)) return false;

            message = "Успешно";
            return true;
        }

        public bool CheckMachineryDTO(MachineryDTO dto, out string message) {
            if (!StringIsNullOrEmptry(dto.Name, out message)) return false;
            if (!StringLenght(dto.Name, out message)) return false;
            if (!CheckZero(dto.TypeId, out message)) return false;

            message = "Успешно";
            return true;
        }


        private bool StringIsNullOrEmptry(string line, out string message) {
            message = "Проверка строки на пустоту или null... ";
            if (String.IsNullOrEmpty(line)) {
                message = "Строка не должна быть пустой";
                return false;
            }
            message += "Успешно \n";
            return true;
        }

        private bool StringLenght(string line, out string message) {
            message = "Проверка длины строки... ";
            if (line.Length < 3) {
                message = "Строка должна содержать 4 и более символов";
                return false;
            }
            message += "Успешно \n";
            return true;
        }
        private bool CheckZero(int num, out string message) {
            message = "Проверка на равенство нулю... ";
            if (num == 0) {
                message = "Не должно быть равно нулю";
                return false;
            }
            message += "Успешно \n";
            return true;
        }

        private bool CheckListCountZero<T>(IList<T> list, out string message) {

            message = "Проверка списка... ";
            if (list == null || list.Count == 0) {
                message = "Список должен содержать элементы";
                return false;
            }
            message += "Успешно \n";
            return true;

        }
    }
}
