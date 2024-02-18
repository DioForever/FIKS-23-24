int start()
{
  char v1; // bl
  char Buffer[2]; // [esp+8h] [ebp-114h] BYREF
  char v3; // [esp+Ah] [ebp-112h]
  char v4; // [esp+Bh] [ebp-111h]
  unsigned __int8 v5; // [esp+Ch] [ebp-110h]
  unsigned __int8 v6; // [esp+Dh] [ebp-10Fh]
  char v7; // [esp+Eh] [ebp-10Eh]
  unsigned __int8 v8; // [esp+Fh] [ebp-10Dh]
  char v9; // [esp+10h] [ebp-10Ch]
  void *lpBuffer; // [esp+108h] [ebp-14h]
  char v11[2]; // [esp+10Ch] [ebp-10h]
  char v12; // [esp+10Eh] [ebp-Eh]
  char v13; // [esp+10Fh] [ebp-Dh]
  char v14; // [esp+110h] [ebp-Ch]
  char v15; // [esp+111h] [ebp-Bh]
  char v16; // [esp+112h] [ebp-Ah]
  char v17; // [esp+113h] [ebp-9h]
  char v18; // [esp+114h] [ebp-8h]
  char v19; // [esp+115h] [ebp-7h]
  unsigned __int16 i; // [esp+118h] [ebp-4h]

  lpBuffer = aVTomhleProgram;
  PrintTextToConsole(aVTomhleProgram);
  if ( !readUserInput(Buffer) )
    return fail_length();
  Sleep(0xFAu);
  if ( length_counter(Buffer) > 10 )
    return fail_length();
  if ( v9 )
    return fail_password();
  v18 = ~v7;
  v15 = (v6 >> (v8 % 5) << (v8 % 5)) + (v5 & 0xF);
  v12 = 4 * v3;
  v17 = 2 * v8 - *(lpBuffer + 9);
  v13 = v8 + v4;
  Sleep(0xFAu);
  v11[0] = Buffer[0] + 23;
  v14 = (v6 & 0xF) + (v5 >> (v8 % 5) << (v8 % 5));
  v12 -= 2 * v3;
  v16 = Buffer[0] - v3;
  v1 = Buffer[1];
  v11[1] = encryption(a96) ^ v1;
  Sleep(0xFAu);
  v19 = v9;
  for ( i = 0; i < 0xAu; ++i )
  {
    if ( v11[i] != byte_402024[i] )
      return fail_password();
  }
  Sleep(0xFAu);
  return CorrectPassword(Buffer);
}