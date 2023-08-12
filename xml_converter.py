import pandas as pd
import xml.etree.ElementTree as gfg

df = pd.read_excel('weapons.xlsx', dtype=str)

df['ATTACHMENTS'].fillna('', inplace=True)

def mapdf(s):
    if '.' not in s:
        return s
    return s[:s.index('.')]

for col in df.columns:
    df[col] = df[col].map(mapdf)


items = gfg.Element('Weapons')

for row in df.iloc:
    item = gfg.SubElement(items, 'Weapon')
    gun = gfg.SubElement(item, 'GunId')
    gun.text = row['GUN_ID']
    ammo = gfg.SubElement(item, 'AmmoId')
    ammo.text = row['AMMO_ID']
    weight = gfg.SubElement(item, 'Weight')
    weight.text = row['WEIGHT']

    attachments = gfg.SubElement(item, 'Attachments')
    
    for a in row['ATTACHMENTS'].split(';'):
        if not a:
            continue
        attachment = gfg.SubElement(attachments, 'Attachment')
        attachment.text = str(a)

      
tree = gfg.ElementTree(items)
gfg.indent(tree, '\t')
  
with open('weapons.xml', "wb") as f:
    tree.write(f)

