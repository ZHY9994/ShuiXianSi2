import os

def is_text_file(file_path):
    """判断文件是否为文本文件（通过检测前几百字节是否包含非文本字符）"""
    try:
        with open(file_path, 'rb') as f:
            chunk = f.read(1024)
        # 如果有超过30%的字节是不可打印字符，就认为是二进制文件
        non_text = sum(b < 9 or (13 < b < 32) or b > 126 for b in chunk)
        return non_text / max(1, len(chunk)) < 0.3
    except:
        return False


def replace_script_in_prefab(file_path):
    old_script = "postProcessData: {fileID: 11400000, guid: 5cb71495aec174c42a76fa951dfefa56, type: 2}"
    new_script = "postProcessData: {fileID: 11400000, guid: 41439944d30ece34e96484bdb6645b55, type: 2}"

    if not is_text_file(file_path):
        print(f"跳过二进制文件: {file_path}")
        return

    try:
        with open(file_path, 'r', encoding='utf-8') as file:
            content = file.read()

        if old_script in content:
            content = content.replace(old_script, new_script)
            with open(file_path, 'w', encoding='utf-8') as file:
                file.write(content)
            print(f"替换成功: {file_path}")

    except Exception as e:
        print(f"处理文件 {file_path} 时出错: {e}")


def process_prefabs_in_folder(folder_path):
    exts = ['.unity', '.prefab', '.asset']
    for root, _, files in os.walk(folder_path):
        for file in files:
            if any(file.endswith(ext) for ext in exts):
                file_path = os.path.join(root, file)
                replace_script_in_prefab(file_path)


if __name__ == "__main__":
    process_prefabs_in_folder(r"D:\Downloads\OPUS2\ExportedProject\Assets")
