#include <stdio.h>
#include <io.h>
#include <stdlib.h>
#include <windows.h>

void set_title(char* const title);  // ���ô��ڱ���
char* get_errmsg(DWORD dwErrCode);  // ��ӡ����
void print_error();                 // ��ȡ�������ԵĴ�����Ϣ

#define DRAKRED  FOREGROUND_RED | FOREGROUND_INTENSITY
#define RED  FOREGROUND_RED
#define WHITE   FOREGROUND_GREEN | FOREGROUND_RED | FOREGROUND_BLUE

int main(int argc, char** argv)
{
    if (argc < 3)
    {
        puts("��������");
        puts("ʹ�÷������������ļ�.exe <���ڱ���> <Դ�ļ�·��> <Ŀ���ļ�·��>");
        system("pause");
        return -1;
    }

    char* const title = argv[1];// Ҫ���õĴ��ڱ���
    const char* source = argv[2]; // Դ�ļ�·��
    const char* dest = argv[3];   // Ŀ���ļ�·��
    char drive[MAX_PATH], dir[MAX_PATH], tmp[MAX_PATH];
    STARTUPINFO si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    ZeroMemory(&pi, sizeof(pi));
    _splitpath_s(dest, drive, sizeof(drive), dir, sizeof(dir), NULL, 0, NULL, 0);
    snprintf(tmp, sizeof(tmp), "%s%s", drive, dir);

    Sleep(1000);// ��ʱ�ȴ�����˳�
    set_title(title);
    // ���·���Ƿ���ȷ
    if (_access(source, 0))
    {
        puts("Դ�ļ�·�������ڣ�");
        puts(source);
        puts("ʹ�÷������滻�ļ�.exe <���ڱ���> <Դ�ļ�·��> <Ŀ���ļ�·��>");
        system("pause");
        return -1;
    }
    // �ж�Ŀ��·���ļ��Ƿ����
    if (!_access(dest, 0))
    {
        // ɾ���Ѵ��ڵ�Ŀ���ļ�
        if (remove(dest))
        {
            print_error();
            system("pause");
            return -1;
        }
    }
    // �������ļ�
    if (rename(source, dest))
    {
        print_error();
        system("pause");
        return -1;
    }

    // ������������ĳ���
    CreateProcessA(dest, NULL, NULL, NULL, FALSE, 0, NULL, tmp, &si, &pi);
    return 0;
}

// ���ô��ڱ���
void set_title(char* const title)
{
    char cmd[MAX_PATH];
    snprintf(cmd, sizeof(cmd), "title %s", title);
    system(cmd);
}

// ��ӡ����
void print_error()
{
    char* msg = get_errmsg(GetLastError());
    // ����������ɫ
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), RED);
    puts(msg);// ��ӡ��������
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), WHITE);
    LocalFree(msg);// �ͷŴ���������ռ���ڴ�
}

// ��ȡ�������ԵĴ�����Ϣ
char* get_errmsg(DWORD dwErrCode)
{
    char* szBuf;
    FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER
        | FORMAT_MESSAGE_FROM_SYSTEM
        | FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL, dwErrCode, LANG_USER_DEFAULT, (LPSTR)&szBuf, 0, NULL);
    return szBuf;
}