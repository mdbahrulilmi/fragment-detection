using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace fragment_detection.Helpers
{
    public class NavigationService
    {
        private readonly Stack<UserControl> _navigationStack = new Stack<UserControl>();

        // Menavigasi ke halaman baru
        public void Navigate(UserControl control)
        {
            // Simpan halaman saat ini sebelum menavigasi
            if (_navigationStack.Count > 0)
            {
                _navigationStack.Push(_navigationStack.Peek()); // Menambahkan halaman sebelumnya
            }

            _navigationStack.Push(control);
        }

        // Kembali ke halaman sebelumnya
        public UserControl? GoBack()
        {
            if (_navigationStack.Count > 1) // Jika ada halaman sebelumnya
            {
                _navigationStack.Pop(); // Hapus halaman saat ini
                return _navigationStack.Peek(); // Kembalikan halaman sebelumnya
            }
            return null; // Tidak ada halaman untuk kembali
        }

        // Mengambil halaman saat ini tanpa menghapusnya
        public UserControl? GetCurrentPage()
        {
            return _navigationStack.Peek();
        }
    }

}
