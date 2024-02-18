#!/usr/bin/env python3
import sys
def l(fp) -> bytes:
    with open(fp, 'rb') as f:
        c = f.read()
    try:
        c.decode('utf-8')
        e = 'utf-8'
    except UnicodeDecodeError:
        try:
            c.decode('utf-16')
            e = 'utf-16'
        except UnicodeDecodeError:
            print("File is not UTF-8 nor UTF-16 encoded")
            sys.exit(2)
    if e == 'utf-16':
        c = c.decode('utf-16').encode('utf-8')
    c = c.replace(b'\r\n', b'\n')
    return c

def c_h(ff, hv):
    import hashlib
    fs = l(ff).decode('utf-8').strip().split('\n')

    e = []

    pps = 0
    for f in fs:
        if f in e:
            continue
        e.append(f)
        sed = f.strip()
        f_h = hashlib.sha512(sed.encode('utf-8')).hexdigest()
        for h_pp in hv:
            h, p = h_pp.strip().split(':')
            if f_h == h.strip():
                print(f'Flag {f} is correct.')
                pps += int(p)
                break
        else:
            print(f'Flag {f} is WRONG!')

    print(f'Bodu: {min(pps,10)}/10')

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python checker.py your_flags_file < .hashes.txt")
        sys.exit(1)
    ff = sys.argv[1]
    hv = sys.stdin.readlines()
    c_h(ff, hv)
