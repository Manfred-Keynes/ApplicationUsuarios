PGDMP     8                    z            ApplicationUsuarios    14.2    14.2     ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    16409    ApplicationUsuarios    DATABASE     q   CREATE DATABASE "ApplicationUsuarios" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Spain.1252';
 %   DROP DATABASE "ApplicationUsuarios";
                postgres    false                        2615    16410    usuario    SCHEMA        CREATE SCHEMA usuario;
    DROP SCHEMA usuario;
                postgres    false            ?            1259    16421    prueba    TABLE     O   CREATE TABLE usuario.prueba (
    id integer NOT NULL,
    descripcion text
);
    DROP TABLE usuario.prueba;
       usuario         heap    postgres    false    5            ?            1259    16424    prueba_id_seq    SEQUENCE     ?   ALTER TABLE usuario.prueba ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME usuario.prueba_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            usuario          postgres    false    211    5            ?            1259    16411    usuario    TABLE       CREATE TABLE usuario.usuario (
    id bigint NOT NULL,
    nombre text,
    telefono text,
    "Puesto" text,
    "Email" text,
    "Contrasenia" text,
    "Usuario" text,
    "fechaCreacion" timestamp with time zone DEFAULT now(),
    "fechaNacimiento" date,
    apellido text
);
    DROP TABLE usuario.usuario;
       usuario         heap    postgres    false    5            ?            1259    16417    usuarios_id_seq    SEQUENCE     ?   ALTER TABLE usuario.usuario ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME usuario.usuarios_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            usuario          postgres    false    5    209            ?          0    16421    prueba 
   TABLE DATA           2   COPY usuario.prueba (id, descripcion) FROM stdin;
    usuario          postgres    false    211   R       ?          0    16411    usuario 
   TABLE DATA           ?   COPY usuario.usuario (id, nombre, telefono, "Puesto", "Email", "Contrasenia", "Usuario", "fechaCreacion", "fechaNacimiento", apellido) FROM stdin;
    usuario          postgres    false    209   o       ?           0    0    prueba_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('usuario.prueba_id_seq', 1, false);
          usuario          postgres    false    212            ?           0    0    usuarios_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('usuario.usuarios_id_seq', 17, true);
          usuario          postgres    false    210            e           2606    16431    prueba prueba_pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY usuario.prueba
    ADD CONSTRAINT prueba_pkey PRIMARY KEY (id);
 =   ALTER TABLE ONLY usuario.prueba DROP CONSTRAINT prueba_pkey;
       usuario            postgres    false    211            c           2606    16419    usuario usuarios_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY usuario.usuario
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY usuario.usuario DROP CONSTRAINT usuarios_pkey;
       usuario            postgres    false    209            ?      x?????? ? ?      ?   ,  x????n?@?????`?Ξ}U?Җ*I+?P/r3?6??bjy?.6`?R??"Y?%???柑d\??֡?f??V8p??$_?rQ???"i?b?X?[?i?&O??(???!??	??P?2?H3????>?<?????
7/??@???SW???և??V?O?&??gU???ʤ???????nS?i?oDQd??UѴ??*?m?eR?%??R?????_?c?+磏?d?@~G<71?E/?!?P??:d????K??L????????*sX+????ZB2Q+@!U?쌒?>l?x????VB?"????~?Y?sS\????޸c?^h!J???r?٬/E??|????Y+{???y??N?X89R?P5dQ?P?p3Rt???0d<?̦??ϳ?'?l?.???O??]uL??? ?? e?2????]??g?#?????pH???R?,e?*????RG?e??'b)?????۞ݞ??n??*????S?x
9*?`h??J??Bf?¢?)?PM-p?R?d>??{?:yMﾽ?U???Xqj5;?Zs??_z???     