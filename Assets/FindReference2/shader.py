import os
import re

def parse_script_file(file_path):
    """解析 script 文件，返回 AssetType 到 (guid, fileID) 的映射"""
    result = {}
    try:
        with open(file_path, 'r', encoding='utf-8') as file:
            for line in file:
                line = line.strip()
                if not line or ':' not in line:
                    continue
                # 匹配格式: AssetType:guid[/fileID]
                parts = line.split(':')
                if len(parts) != 2:
                    print(f"警告: 跳过无效行 {line} 在 {file_path}")
                    continue
                asset_type = parts[0].strip()
                value = parts[1].strip()
                guid = value.split('/')[0] if '/' in value else value
                file_id = value.split('/')[1] if '/' in value else '11500000'
                result[asset_type] = (guid, file_id)
        return result
    except FileNotFoundError:
        print(f"错误: 文件 {file_path} 不存在")
        return None
    except Exception as e:
        print(f"解析文件 {file_path} 时出错: {e}")
        return None

def replace_script_in_file(file_path, old_scripts, new_scripts):
    """替换文件中指定 m_Script 内容"""
    try:
        with open(file_path, 'r', encoding='utf-8') as file:
            content = file.read()

        modified = False
        for asset_type, (old_guid, old_file_id) in old_scripts.items():
            if asset_type not in new_scripts:
                print(f"警告: {asset_type} 在 new_script.txt 中未找到，跳过")
                continue
            new_guid, _ = new_scripts[asset_type]
            old_m_script = f"m_Shader: {{fileID: {old_file_id}, guid: {old_guid}, type: 3}}"
            new_m_script = f"m_Shader: {{fileID: 4800000, guid: 47c27489940963c46b02e04b9fc8fc8a, type: 3}}"
            if old_m_script in content:
                content = content.replace(old_m_script, new_m_script)
                modified = True
                print(f"替换 {asset_type} 在 {file_path}: {old_m_script} -> {new_m_script}")
                

        if modified:
            with open(file_path, 'w', encoding='utf-8') as file:
                file.write(content)
            print(f"in: {file_path}")
        

    except PermissionError:
        print(f"权限错误，无法访问文件: {file_path}")
    except UnicodeDecodeError:
        print(f"编码错误，无法读取文件: {file_path}")
    except Exception as e:
        print(f"处理文件 {file_path} 时出错: {e}")

def process_files_in_folder(folder_path, old_script_file, new_script_file):
    """递归遍历文件夹，处理指定扩展名的文件"""
    # 解析 old_script.txt 和 new_script.txt
    old_scripts = parse_script_file(old_script_file)
    new_scripts = parse_script_file(new_script_file)
    
    if not old_scripts or not new_scripts:
        print("无法继续：脚本文件解析失败")
        return
    
    # 检查是否有可匹配的 AssetType
    common_asset_types = set(old_scripts.keys()) & set(new_scripts.keys())
    if not common_asset_types:
        print("错误：old_script.txt 和 new_script.txt 没有共同的 AssetType")
        return

    target_extensions = {'.unitys', '.prefabs', '.assets', '.mat'}

    for root, _, files in os.walk(folder_path):
        for file in files:
            if any(file.endswith(ext) for ext in target_extensions):
                file_path = os.path.join(root, file)
                replace_script_in_file(file_path, old_scripts, new_scripts)

if __name__ == "__main__":
    folder_path = input("请输入文件夹路径（留空使用默认路径）: ") or r'D:\Downloads\DUCA\ExportedProject\Assets'
    old_script_file = input("请输入 old_script.txt 文件路径: ") or r'D:\Downloads\DUCA\ExportedProject\Assets\FindReference2\input.txt'
    new_script_file = input("请输入 new_script.txt 文件路径: ") or r'D:\Downloads\DUCA\ExportedProject\Assets\FindReference2\output.txt'

    if not os.path.exists(folder_path):
        print(f"错误：路径 {folder_path} 不存在！")
    elif not os.path.exists(old_script_file) or not os.path.exists(new_script_file):
        print("错误：old_script.txt 或 new_script.txt 文件不存在！")
    else:
        process_files_in_folder(folder_path, old_script_file, new_script_file)
