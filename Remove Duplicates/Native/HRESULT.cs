//
//    Remove Duplicates
//    Copyright (C) 2021 Timothy Baxendale
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
namespace Baxendale.RemoveDuplicates.Native
{
    internal enum HRESULT : uint
    {
        S_OK            = 0x00000000, // Operation successful
        E_NOTIMPL       = 0x80004001, // Not implemented
        E_NOINTERFACE   = 0x80004002, // No such interface supported
        E_POINTER       = 0x80004003, // Pointer that is not valid
        E_ABORT         = 0x80004004, // Operation aborted
        E_FAIL          = 0x80004005, // Unspecified failure
        E_UNEXPECTED    = 0x8000FFFF, // Unexpected failure
        E_ACCESSDENIED  = 0x80070005, // General access denied error
        E_HANDLE        = 0x80070006, // Handle that is not valid
        E_OUTOFMEMORY   = 0x8007000E, // Failed to allocate necessary memory
        E_INVALIDARG    = 0x80070057, // One or more arguments are not valid
    }
}
