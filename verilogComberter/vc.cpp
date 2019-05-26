#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <iostream>

FILE* fp;
FILE* fpv;

void openFile(); 
void mkVTop();
void mkfunction();
void closeFile();

int w, h;

int main()
{
	openFile();
	mkVTop();
	mkfunction();
	closeFile();

	system("pause");
	return 0;
}

void closeFile()
{
	fclose(fp);

	fprintf(fpv, "endmodule");
	fclose(fpv);
}

void openFile()
{
	fp = fopen("minitsuku.pix", "r");

	fscanf(fp, "%d", &w);
	fscanf(fp, "%d", &h);
	printf("%d x %d\n", w, h);

	fpv = fopen("pixel.v", "w");
}

void mkVTop()
{
	fprintf(fpv, "//PIXEL DATA\n\n");

	fprintf(fpv, "module pixel\n");

	fprintf(fpv, "#(parameter integer HCOUNT_WIDTH = -1, parameter integer VCOUNT_WIDTH = -1)\n");
	fprintf(fpv, "(h, v, r, g, b);\n\n");

	fprintf(fpv, "input wire [HCOUNT_WIDTH-1:0] h;\n");
	fprintf(fpv, "input wire [VCOUNT_WIDTH-1:0] v;\n\n");

	fprintf(fpv, "output wire [7:0] ro, go, bo;\n\n");

	fprintf(fpv, "assign ro = pr(h, v);\n");
	fprintf(fpv, "assign go = pg(h, v);\n");
	fprintf(fpv, "assign bo = pb(h, v);\n\n");

	fprintf(fpv, "// %d x %d\n\n", w, h);
}

void mkfunction()
{
	fprintf(fpv, "function [7:0] pr;\n");
	fprintf(fpv, "input [HCOUNT_WIDTH-1:0] hi;\n");
	fprintf(fpv, "input [VCOUNT_WIDTH-1:0] vi;\nbegin\n");

	for (int y = 0; y < h; y++)
	{
		for (int x = 0; x < w; x++)
		{
			if (x == 0 && y == 0)
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "if(h == 0 & v == 0) begin\n");
				fprintf(fpv, "pr = %d; end\n", val);
			}
			else
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "else if(h == %d & v == %d) begin\n", x, y);
				fprintf(fpv, "pr = %d; end\n", val);
			}
		}
	}
	fprintf(fpv, "else begin\n");
	fprintf(fpv, "pr = 0; end\n");
	fprintf(fpv, "endfunction\n\n");

	fprintf(fpv, "function [7:0] pg;\n");
	fprintf(fpv, "input [HCOUNT_WIDTH-1:0] hi;\n");
	fprintf(fpv, "input [VCOUNT_WIDTH-1:0] vi;\nbegin\n");

	for (int y = 0; y < h; y++)
	{
		for (int x = 0; x < w; x++)
		{
			if (x == 0 && y == 0)
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "if(h == 0 & v == 0) begin\n");
				fprintf(fpv, "pg = %d; end\n", val);
			}
			else
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "else if(h == %d & v == %d) begin\n", x, y);
				fprintf(fpv, "pg = %d; end\n", val);
			}
		}
	}
	fprintf(fpv, "else begin\n");
	fprintf(fpv, "pg = 0; end\n");
	fprintf(fpv, "endfunction\n\n");

	fprintf(fpv, "function [7:0] pb;\n");
	fprintf(fpv, "input [HCOUNT_WIDTH-1:0] hi;\n");
	fprintf(fpv, "input [VCOUNT_WIDTH-1:0] vi;\nbegin\n");

	for (int y = 0; y < h; y++)
	{
		for (int x = 0; x < w; x++)
		{
			if (x == 0 && y == 0)
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "if(h == 0 & v == 0) begin\n");
				fprintf(fpv, "pb = %d; end\n", val);
			}
			else
			{
				int val;
				fscanf(fp, "%d", &val);
				fprintf(fpv, "else if(h == %d & v == %d) begin\n", x, y);
				fprintf(fpv, "pb = %d; end\n", val);
			}
		}
	}
	fprintf(fpv, "else begin\n");
	fprintf(fpv, "pb = 0; end\n");
	fprintf(fpv, "endfunction\n\n");



}