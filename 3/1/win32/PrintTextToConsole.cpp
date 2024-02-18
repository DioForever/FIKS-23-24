BOOL __cdecl PrintTextToConsole(void *lpBuffer)
{
  DWORD v1; // eax
  HANDLE hConsoleOutput; // [esp+0h] [ebp-8h]
  DWORD NumberOfCharsWritten; // [esp+4h] [ebp-4h] BYREF

  NumberOfCharsWritten = 0;
  hConsoleOutput = GetStdHandle(0xFFFFFFF5);
  v1 = length_counter(lpBuffer);
  return WriteConsoleA(hConsoleOutput, lpBuffer, v1, &NumberOfCharsWritten, 0);
}